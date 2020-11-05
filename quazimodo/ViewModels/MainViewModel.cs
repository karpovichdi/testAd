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
                case SoundParameter.Wow:
                    _audioService.PlayAudioFile(SoundParameter.Wow + _extension);
                    break;
                case SoundParameter.Star:
                    _audioService.PlayAudioFile(SoundParameter.Star + _extension);
                    break;
                case SoundParameter.wink:
                    _audioService.PlayAudioFile(SoundParameter.wink + _extension);
                    break;
                case SoundParameter.thinking:
                    _audioService.PlayAudioFile(SoundParameter.thinking + _extension);
                    break;
                case SoundParameter.confusingq:
                    _audioService.PlayAudioFile(SoundParameter.confusingq + _extension);
                    break;
                case SoundParameter.blowkiss:
                    _audioService.PlayAudioFile(SoundParameter.blowkiss + _extension);
                    break;
                case SoundParameter.grandfather:
                    _audioService.PlayAudioFile(SoundParameter.grandfather + _extension);
                    break;
                case SoundParameter.sweating:
                    _audioService.PlayAudioFile(SoundParameter.sweating + _extension);
                    break;
                case SoundParameter.laughingq:
                    _audioService.PlayAudioFile(SoundParameter.laughingq + _extension);
                    break;
                case SoundParameter.neutral:
                    _audioService.PlayAudioFile(SoundParameter.neutral + _extension);
                    break;
                case SoundParameter.grinningq:
                    _audioService.PlayAudioFile(SoundParameter.grinningq + _extension);
                    break;
                case SoundParameter.skull:
                    _audioService.PlayAudioFile(SoundParameter.skull + _extension);
                    break;
                case SoundParameter.sneezing:
                    _audioService.PlayAudioFile(SoundParameter.sneezing + _extension);
                    break;
                case SoundParameter.flushed:
                    _audioService.PlayAudioFile(SoundParameter.flushed + _extension);
                    break;
                case SoundParameter.joy:
                    _audioService.PlayAudioFile(SoundParameter.joy + _extension);
                    break;
                case SoundParameter.silentq:
                    _audioService.PlayAudioFile(SoundParameter.silentq + _extension);
                    break;
                case SoundParameter.sad:
                    _audioService.PlayAudioFile(SoundParameter.sad + _extension);
                    break;
                case SoundParameter.tears:
                    _audioService.PlayAudioFile(SoundParameter.tears + _extension);
                    break;
                case SoundParameter.amazed:
                    _audioService.PlayAudioFile(SoundParameter.amazed + _extension);
                    break;
            }
        }
    }
}