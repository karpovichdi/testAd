using System;
using quazimodo.Constants;
using quazimodo.Enums;
using quazimodo.Interfaces;
using quazimodo.Views;
using Xamarin.Forms;

namespace quazimodo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Command MusicBtnClickCommand { get; set; }
        public Command StopCommand { get; set; }

        private IAudioService _audioService;

        private string _extension = ".mp3";
        
        private bool _stopButtonVisible;

        public bool StopButtonVisible
        {
            get => _stopButtonVisible;
            set
            {
                _stopButtonVisible = value;
                OnPropertyChanged(nameof(StopButtonVisible));
            }
        }

        public MainViewModel()
        {
            MusicBtnClickCommand = new Command(SmileClickCommandHandler);
            StopCommand = new Command(StopCommandHandlder);
            
            _audioService = DependencyService.Get<IAudioService>();
            
            MessagingCenter.Instance.Subscribe<byte[]>(this, MessagingCenterConstants.LastSongFinished, (array) => LastSoundStopped());
        }

        private void LastSoundStopped()
        {
            StopButtonVisible = false;
        }

        private void StopCommandHandlder(object obj)
        {
            _audioService.StopPlaying();
            StopButtonVisible = false;
        }

        private void SmileClickCommandHandler(object obj)
        {
            if(obj is string) return;
            var parameter = (SoundParameter)obj;

            StopButtonVisible = true;
            
            switch (parameter)
            {
                case SoundParameter.Reject:
                    _audioService.PlayAudioFile(SoundParameter.Reject + _extension);
                    break;
                case SoundParameter.Accept:
                    _audioService.PlayAudioFile(SoundParameter.Accept + _extension);
                    break;
                case SoundParameter.Love:
                    _audioService.PlayAudioFile(SoundParameter.Love + _extension);
                    break;
                case SoundParameter.Crank:
                    _audioService.PlayAudioFile(SoundParameter.Crank + _extension);
                    break;
                case SoundParameter.Creepypasta:
                    _audioService.PlayAudioFile(SoundParameter.Creepypasta + _extension);
                    break;
                default: _audioService.PlayAudioFile("ILoveYou" + _extension);
                    break;
            }
        }
    }
}