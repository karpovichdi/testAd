using System.IO;
using AVFoundation;
using Foundation;
using quazimodo.iOS;
using Xamarin.Forms; 

[assembly: Dependency(typeof(AudioService))]
namespace quazimodo.iOS
{
    public class AudioService : IAudio
    {
        private AVAudioPlayer _player;

        public AudioService()
        {
            
        }
        
        public void PlayAudioFile(string fileName)
        {
            string sFilePath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            NSUrl url = NSUrl.FromString(sFilePath);
            _player = AVAudioPlayer.FromUrl(url);
            _player.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
            {
                _player = null;
            };
            _player.Play();
        }

        public void StopPlaying()
        {
            _player.Stop();
        }
    } 
}