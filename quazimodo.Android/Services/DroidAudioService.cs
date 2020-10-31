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
        private bool _isPlaying;
        
        public void PlayAudioFile(string fileName)
        {
            if (_isPlaying)
            {
                return;
            }

            _isPlaying = true;
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
            if (!_isPlaying)
            {
                return;
            }
            _player.Stop();
            _isPlaying = false;
        }
    }
}