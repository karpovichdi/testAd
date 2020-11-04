using System.Collections.Generic;
using System.Linq;
using Android.Gms.Tasks;
using Android.Media;
using quazimodo.Constants;
using quazimodo.Droid.Services;
using quazimodo.Interfaces;
using quazimodo.ViewModels;
using quazimodo.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidAudioService))]
namespace quazimodo.Droid.Services
{
    public class DroidAudioService : IAudioService
    {
        private MediaPlayer _player;

        private static List<MediaPlayer> ListOfPlayers;

        public DroidAudioService()
        {
            ListOfPlayers = new List<MediaPlayer>();
        }

        public void PlayAudioFile(string fileName)
        {
            _player = new MediaPlayer();

            _player.SetOnCompletionListener(new CompletionListener());
            
            ListOfPlayers.Add(_player);

            if (global::Android.App.Application.Context.Assets != null)
            {
                var fd = global::Android.App.Application.Context.Assets.OpenFd(fileName);
                _player.Prepared += (s, e) =>
                {
                    _player.Start();
                };
                if (fd != null)
                {
                    _player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
                }
            }

            _player.Prepare();
        }

        public void StopPlaying()
        {
            ListOfPlayers.RemoveAll(x => !x.IsPlaying);

            foreach (var player in ListOfPlayers)
            {
                player?.Stop();
            }
            
            ListOfPlayers.RemoveAll(x => !x.IsPlaying);
        }

        private class CompletionListener : Java.Lang.Object, MediaPlayer.IOnCompletionListener
        {
            public void OnCompletion(MediaPlayer? mp)
            {
                if (mp != null)
                {
                    ListOfPlayers.Remove(mp);
                    if (ListOfPlayers.Count == 0)
                    {
                        MessagingCenter.Instance.Send(new byte[0], MessagingCenterConstants.LastSongFinished);
                    }
                }
            }
        }
    }
}