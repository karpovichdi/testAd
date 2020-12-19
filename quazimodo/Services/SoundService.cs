using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using Plugin.SimpleAudioPlayer;
using quazimodo.Constants;
using quazimodo.Enums;
using quazimodo.Interfaces;
using quazimodo.Services;
using quazimodo.Utilities;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(SoundService))]
namespace quazimodo.Services
{
    public class SoundService : ISoundService
    {
        public override Action<SoundParameter> SoundReleased { get; set; }
        public override Action RecordReleased { get; set; }
        public override Action AppClosed { get; set; }

        private readonly List<ISimpleAudioPlayer> _freePlayers = new List<ISimpleAudioPlayer>();
        private readonly Dictionary<ISimpleAudioPlayer, SoundParameter> _busyPlayers = 
            new Dictionary<ISimpleAudioPlayer, SoundParameter>();
        
        private const string SoundExtension = ".mp3";
        
        private AudioRecorderService _recorderService;

        private readonly AudioPlayer _audioPlayer;
        
        public bool CanPlaySound => _freePlayers.Count > 0;

        private string GetPathToSound(SoundParameter parameter)
        {
            return $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/{parameter}";
        }

        //     string path = Environment.ExternalStorageDirectory;
    //     string filename = Path.Combine(path, "myfile.txt");
    //
    //         using (var streamWriter = new StreamWriter(filename, true))
    //     {
    //         streamWriter.WriteLine(DateTime.UtcNow);
    //     }
    //
    // using (var streamReader = new StreamReader(filename))
    // {
    // string content = streamReader.ReadToEnd();
    // System.Diagnostics.Debug.WriteLine(content);
    // }
        
        
        public SoundService()
        {
            for (var i = 0; i < 10; i++)
            {
                var player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                player.PlaybackEnded += PlayerOnPlaybackEnded;
                _freePlayers.Add(player);
            } 
            
            _audioPlayer = new AudioPlayer();
            _audioPlayer.FinishedPlaying += AudioFinishHandler;
        }

        private ISimpleAudioPlayer GetPlayer(SoundParameter soundParameter)
        {
            if (_freePlayers.Count == 0) return null;
            var player = _freePlayers.First();
            _freePlayers.Remove(player);
            _busyPlayers.Add(player, soundParameter);
            return player;
        }
        
        private void ReleasePlayer()
        {
            var players = _busyPlayers.Where(x => !x.Key.IsPlaying);
            var keyValuePairs = players.ToList();
            
            foreach (var player in keyValuePairs)
            {
                _freePlayers.Add(player.Key);
                SoundReleased.Invoke(player.Value);
            }

            var busyPlayersCount = keyValuePairs.Count();
            for (var i = 0; i < busyPlayersCount; i++)
            {
                _busyPlayers.Remove(keyValuePairs.ElementAt(i).Key);
            }
        }

        private void Play(SoundParameter soundParameter)
        {
            var streamSound = ResourceHelper.GetStreamSound(soundParameter + SoundExtension);

            var player = GetPlayer(soundParameter);
            player.Load(streamSound);
            player.Play();
        }

        private void PlayerOnPlaybackEnded(object sender, EventArgs e)
        {
            ReleasePlayer();
        }

        public override async Task CreateSoundPathAndPlay(SoundParameter parameter)
        {
            try
            {
                if (!CanPlaySound) return;
                Play(parameter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void AudioFinishHandler(object sender, EventArgs e)
        {
            _audioPlayer.Play(_recorderService.GetAudioFilePath());
        }

        public override async Task<PermissionStatus> CheckPermissions()
        {
            var microphoneStatus = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (microphoneStatus != PermissionStatus.Granted)
            {
                return await GetPermissions();
            }
            MicrophonePermissionsGranted = true;
            return PermissionStatus.Granted;
        }

        public override async Task StartRecording(SoundParameter commandParameter)
        {
            // var player = GetPlayer(commandParameter);
            //
            // using (var streamReader = new StreamReader(record1))
            // {
            //     var content = await streamReader.ReadToEndAsync();
            //     var streamReaderBaseStream = streamReader.BaseStream;
            //     player.Load(streamReaderBaseStream);
            //     player.Play();
            // }
            
            _recorderService = new AudioRecorderService
            {
                TotalAudioTimeout = TimeSpan.FromSeconds(ConstantsForms.MaxLenghtOfRecordedSoundInSecond),
                FilePath = GetPathToSound(commandParameter),
                StopRecordingOnSilence = false
            };
                
            _recorderService.AudioInputReceived += RecorderServiceOnAudioInputReceived;
            
            await _recorderService.StartRecording();
        }
        
        private void RecorderServiceOnAudioInputReceived(object sender, string e)
        {
            _recorderService.AudioInputReceived -= RecorderServiceOnAudioInputReceived;
            // not finished logic
            RecordReleased.Invoke();
        }

        public override Task StopRecording()
        {
            if (_recorderService.IsRecording)
            {
                _recorderService.StopRecording();
            }

            return Task.CompletedTask;
        }

        private async Task<PermissionStatus> GetPermissions()
        {
            return await Permissions.RequestAsync<Permissions.Microphone>();
        }

        public override async Task StopPlayingAll()
        {
            foreach (var player in _busyPlayers)
            {
                _freePlayers.Add(player.Key);
            }
            
            _busyPlayers.ForEach(x => x.Key.Stop());
            _busyPlayers.Clear();
        }
    }
}