using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using Plugin.SimpleAudioPlayer;
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
        public override Action AppClosed { get; set; }

        private readonly List<ISimpleAudioPlayer> _freePlayers = new List<ISimpleAudioPlayer>();
        private readonly Dictionary<ISimpleAudioPlayer, SoundParameter> _busyPlayers = 
            new Dictionary<ISimpleAudioPlayer, SoundParameter>();
        
        private const string SoundExtension = ".mp3";
        
        private readonly AudioPlayer _audioPlayer;
        private readonly AudioRecorderService _recorderService;
        
        public bool CanPlaySound => _freePlayers.Count > 0;

        public SoundService()
        {
            _recorderService = new AudioRecorderService { TotalAudioTimeout = TimeSpan.FromSeconds(15) };

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

        internal async Task Record()
        {
            if (!_recorderService.IsRecording)
            {
                _recorderService.StartRecording();
            }
            else
            {
                _recorderService.StopRecording();
            }
        }

        public override async Task CheckPermissions()
        {
            var microphoneStatus = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (microphoneStatus != PermissionStatus.Granted)
            {
                await GetPermissions();
            }
            else
            {
                MicrophonePermissionsGranted = true;
            }
        }

        private async Task GetPermissions()
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();
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