using quazimodo.Constants;
using quazimodo.Enums;
using quazimodo.Interfaces;
using Xamarin.Forms;

namespace quazimodo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Command MusicBtnClickCommand { get; set; }

        private IAudioService _audioService;

        private string _extension = ".mp3";

        public MainViewModel()
        {
            MusicBtnClickCommand = new Command(SmileClickCommandHandler);

            _audioService = DependencyService.Get<IAudioService>();
        }

        private void SmileClickCommandHandler(object obj)
        {
            var parameter = (SoundParameter)obj;
            
            switch (parameter)
            {
                case SoundParameter.Reject:
                    _audioService.PlayAudioFile(SoundParameter.Reject + _extension);
                    break;
                case SoundParameter.Accept:
                    _audioService.PlayAudioFile(SoundParameter.Accept + _extension);
                    break;
            }
        }
    }
}