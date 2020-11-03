using quazimodo.Interfaces;
using Xamarin.Forms;

namespace quazimodo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Command MusicBtnClickCommand { get; set; }

        public MainViewModel()
        {
            MusicBtnClickCommand = new Command(SmileClickCommandHandler);
        }

        private void SmileClickCommandHandler(object obj)
        {
            DependencyService.Get<IAudioService>().PlayAudioFile("ILoveYou.mp3");
        }
    }
}