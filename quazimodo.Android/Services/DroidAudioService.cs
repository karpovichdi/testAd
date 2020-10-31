using Android.Media;
using quazimodo.Droid.Services;
using quazimodo.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidAudioService))]
namespace quazimodo.Droid.Services
{
    public class DroidAudioService : IAudioService
    {
        private MediaPlayer _player;
        
        public void PlayAudioFile(string fileName)
        {
            _player = new MediaPlayer();
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
            _player?.Stop();
        }
    }
}