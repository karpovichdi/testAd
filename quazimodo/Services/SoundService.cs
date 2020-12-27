using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using Plugin.SimpleAudioPlayer;
using quazimodo.Models.Enums;
using quazimodo.Services;
using quazimodo.Services.Interfaces;
using quazimodo.Utilities;
using quazimodo.Utilities.Constants;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(SoundService))]
namespace quazimodo.Services
{
    public class SoundService : ISoundService
    {        
        public override Action<SoundParameter> SongReleased { get; set; }
        public override Action RecordReleased { get; set; }
        public override Action AppClosed { get; set; }

        private readonly List<ISimpleAudioPlayer> _freePlayers = new List<ISimpleAudioPlayer>();
        private readonly Dictionary<ISimpleAudioPlayer, PlayerItemModel> _busyPlayers = 
            new Dictionary<ISimpleAudioPlayer, PlayerItemModel>();
        
        private AudioRecorderService _recorderService;

        private readonly AudioPlayer _audioPlayer;

        private bool CanPlaySound => _freePlayers.Count > 0;

        public SoundService()
        {
            for (var i = 0; i < ConstantsForms.MaxCountOfSoundInOneTime; i++)
            {
                var player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                player.PlaybackEnded += PlayerOnPlaybackEnded;
                _freePlayers.Add(player);
            } 
            
            _audioPlayer = new AudioPlayer();
            _audioPlayer.FinishedPlaying += AudioFinishHandler;
        }

        public override async Task CreateSoundPathAndPlay(SoundParameter parameter)
        {
            if (!CanPlaySound) return;
            Play(parameter);
        }

        public override async Task<PermissionStatus> RequestPermissionsIfNeeded()
        {
            var microphoneStatus = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (microphoneStatus != PermissionStatus.Granted)
            {
                return await GetPermissions();
            }
            MicrophonePermissionsGranted = true;
            return PermissionStatus.Granted;
        }

        public override async Task<PermissionStatus> CheckPermissions()
        {
            return await Permissions.CheckStatusAsync<Permissions.Microphone>();
        }

        public override async Task StartRecording(SoundParameter commandParameter)
        {
             _recorderService = new AudioRecorderService
            {
                TotalAudioTimeout = TimeSpan.FromSeconds(ConstantsForms.MaxLenghtOfRecordedSoundInSecond),
                FilePath = Helpers.GetSongPath(commandParameter),
                StopRecordingOnSilence = false,
                StopRecordingAfterTimeout = true 
            };
                
            _recorderService.AudioInputReceived += OnRecordReceived;
            await _recorderService.StartRecording();
        }

        public override async Task StopRecording(bool disableReceiveHandler)
        {
            if (disableReceiveHandler)
            {
                _recorderService.AudioInputReceived -= OnRecordReceived;
            }
            
            if (_recorderService.IsRecording)
            {
                await _recorderService.StopRecording();
                _recorderService = null;
            };
        }

        public override Task DeleteSong(SoundParameter parameter)
        {
            try
            {
                var songPath = Helpers.GetSongPath(parameter);
                var fileExist = System.IO.File.Exists(songPath);
                if (!fileExist) return Task.CompletedTask;
                File.Delete(songPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Task.CompletedTask;
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
        
        private void AudioFinishHandler(object sender, EventArgs e)
        {
            _audioPlayer.Play(_recorderService.GetAudioFilePath());
        }
        
        private void OnRecordReceived(object sender, string e)
        {
            _recorderService.AudioInputReceived -= OnRecordReceived;
            RecordReleased.Invoke();
        }

        private async Task<PermissionStatus> GetPermissions()
        {
            return await Permissions.RequestAsync<Permissions.Microphone>();
        }

        private void PlayerOnPlaybackEnded(object sender, EventArgs e)
        {
            ReleasePlayer();
        }

        private void Play(SoundParameter soundParameter)
        {
            try
            {
                var playingSongStream = Helpers.GetStreamSound(soundParameter) ??
                                        File.Open(Helpers.GetSongPath(soundParameter), FileMode.Open, FileAccess.Read, FileShare.Read);

                var player = GetPlayer(soundParameter, playingSongStream);
                player.Load(playingSongStream);
                player.Play();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void ReleasePlayer()
        {
            var players = _busyPlayers.Where(x => !x.Key.IsPlaying);
            var keyValuePairs = players.ToList();
            
            foreach (var player in keyValuePairs)
            {
                player.Value.Stream.Close();
                _freePlayers.Add(player.Key);
                SongReleased.Invoke(player.Value.Parameter);
            }

            var busyPlayersCount = keyValuePairs.Count();
            for (var i = 0; i < busyPlayersCount; i++)
            {
                _busyPlayers.Remove(keyValuePairs.ElementAt(i).Key);
            }
        }

        private ISimpleAudioPlayer GetPlayer(SoundParameter soundParameter, Stream playingSongStream)
        {
            if (_freePlayers.Count == 0) return null;
            var player = _freePlayers.First();
            _freePlayers.Remove(player);
            _busyPlayers.Add(player, new PlayerItemModel
            { 
                Stream = playingSongStream,
                Parameter = soundParameter
            });
            return player;
        }

        private class PlayerItemModel
        {
            public Stream Stream { get; set; }
            public SoundParameter Parameter { get; set; }
        }
    }
}