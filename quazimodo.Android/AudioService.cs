using Xamarin.Forms;
using Android.Media;
using quazimodo.Droid;

[assembly: Dependency(typeof(AudioService))]
namespace quazimodo.Droid
{
    public class AudioService : IAudio
    {
        private MediaPlayer _player;
        private bool _isPlaying;

        public AudioService()
        {
            
        }
        
        public void PlayAudioFile(string fileName)
        {
            if (_isPlaying)
            {
                return;
            }

            _isPlaying = true;
            _player = new MediaPlayer();
            var fd = global::Android.App.Application.Context.Assets.OpenFd(fileName);
            _player.Prepared += (s, e) =>
            {
                _player.Start();
            };
            _player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
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