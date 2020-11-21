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
        public Command HidePopupCommand { get; set; }
        
        private IAudioService _audioService;

        private string _extension = ".mp3";
        
        private bool _stopButtonVisible;
        private bool _popupVisible;

        public bool StopButtonVisible
        {
            get => _stopButtonVisible;
            set
            {
                _stopButtonVisible = value;
                OnPropertyChanged(nameof(StopButtonVisible));
            }
        }
        
        public bool PopupVisible
        {
            get => _popupVisible;
            set
            {
                _popupVisible = value;
                OnPropertyChanged(nameof(PopupVisible));
            }
        }

        public MainViewModel()
        {
            MusicBtnClickCommand = new Command(SmileClickCommandHandler);
            StopCommand = new Command(StopCommandHandler);
            HidePopupCommand = new Command(HidePopupCommandHandler);
            
            _audioService = DependencyService.Get<IAudioService>();
            
            MessagingCenter.Instance.Subscribe<byte[]>(this, MessagingCenterConstants.LastSongFinished, (array) => LastSoundStopped());
        }

        private void HidePopupCommandHandler()
        {
            PopupVisible = false;
        }
        
        private void LastSoundStopped()
        {
            StopButtonVisible = false;
        }

        private void StopCommandHandler(object obj)
        {
            _audioService.StopPlaying();
            StopButtonVisible = false;
        }

        private void SmileClickCommandHandler(object obj)
        {
            if(obj is string) return;
            var parameter = (SoundParameter)obj;

            StopButtonVisible = true;
            App.CountOfPlayedSound++;

            if (App.CountOfPlayedSound == AdConstants.ClicksCountBeforeAd)
            {
                PopupVisible = true;
                App.CountOfPlayedSound = 0;
            }

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
                case SoundParameter.lyingq:
                    _audioService.PlayAudioFile(SoundParameter.lyingq + _extension);
                    break;
                case SoundParameter.smilingface:
                    _audioService.PlayAudioFile(SoundParameter.smilingface + _extension);
                    break;
                case SoundParameter.teasing:
                    _audioService.PlayAudioFile(SoundParameter.teasing + _extension);
                    break;
                case SoundParameter.surprised:
                    _audioService.PlayAudioFile(SoundParameter.surprised + _extension);
                    break;
                case SoundParameter.dizzy:
                    _audioService.PlayAudioFile(SoundParameter.dizzy + _extension);
                    break;
                case SoundParameter.angry:
                    _audioService.PlayAudioFile(SoundParameter.angry + _extension);
                    break;
                case SoundParameter.mouth:
                    _audioService.PlayAudioFile(SoundParameter.mouth + _extension);
                    break;
                case SoundParameter.alien:
                    _audioService.PlayAudioFile(SoundParameter.alien + _extension);
                    break;
                case SoundParameter.zanyq:
                    _audioService.PlayAudioFile(SoundParameter.zanyq + _extension);
                    break;
                case SoundParameter.smile:
                    _audioService.PlayAudioFile(SoundParameter.smile + _extension);
                    break;
                case SoundParameter.sunglassesq:
                    _audioService.PlayAudioFile(SoundParameter.sunglassesq + _extension);
                    break;
                case SoundParameter.poop:
                    _audioService.PlayAudioFile(SoundParameter.poop + _extension);
                    break;
                case SoundParameter.flatmouth:
                    _audioService.PlayAudioFile(SoundParameter.flatmouth + _extension);
                    break;
                case SoundParameter.eyepatch:
                    _audioService.PlayAudioFile(SoundParameter.eyepatch + _extension);
                    break;
                case SoundParameter.Laughterq:
                    _audioService.PlayAudioFile(SoundParameter.Laughterq + _extension);
                    break;
                case SoundParameter.money:
                    _audioService.PlayAudioFile(SoundParameter.money + _extension);
                    break;
                case SoundParameter.dollar:
                    _audioService.PlayAudioFile(SoundParameter.dollar + _extension);
                    break;
                case SoundParameter.upset:
                    _audioService.PlayAudioFile(SoundParameter.upset + _extension);
                    break;
                case SoundParameter.grimacing:
                    _audioService.PlayAudioFile(SoundParameter.grimacing + _extension);
                    break;
                case SoundParameter.cryd:
                    _audioService.PlayAudioFile(SoundParameter.cryd + _extension);
                    break;
                case SoundParameter.lol:
                    _audioService.PlayAudioFile(SoundParameter.lol + _extension);
                    break;
                case SoundParameter.sunglassesqq:
                    _audioService.PlayAudioFile(SoundParameter.sunglassesqq + _extension);
                    break;
                case SoundParameter.mouthfull:
                    _audioService.PlayAudioFile(SoundParameter.mouthfull + _extension);
                    break;
                case SoundParameter.edevil:
                    _audioService.PlayAudioFile(SoundParameter.edevil + _extension);
                    break;
                case SoundParameter.angryface:
                    _audioService.PlayAudioFile(SoundParameter.angryface + _extension);
                    break;
                case SoundParameter.sleeping:
                    _audioService.PlayAudioFile(SoundParameter.sleeping + _extension);
                    break;
                case SoundParameter.tongueout:
                    _audioService.PlayAudioFile(SoundParameter.tongueout + _extension);
                    break;
                case SoundParameter.Laughter:
                    _audioService.PlayAudioFile(SoundParameter.Laughter + _extension);
                    break;
                case SoundParameter.angel:
                    _audioService.PlayAudioFile(SoundParameter.angel + _extension);
                    break;
                case SoundParameter.policeman:
                    _audioService.PlayAudioFile(SoundParameter.policeman + _extension);
                    break;
                case SoundParameter.thief:
                    _audioService.PlayAudioFile(SoundParameter.thief + _extension);
                    break;
                case SoundParameter.devilsmile:
                    _audioService.PlayAudioFile(SoundParameter.devilsmile + _extension);
                    break;
                case SoundParameter.disturb:
                    _audioService.PlayAudioFile(SoundParameter.disturb + _extension);
                    break;
                case SoundParameter.medicalmask:
                    _audioService.PlayAudioFile(SoundParameter.medicalmask + _extension);
                    break;
                case SoundParameter.expressionless:
                    _audioService.PlayAudioFile(SoundParameter.expressionless + _extension);
                    break;
                case SoundParameter.pirate:
                    _audioService.PlayAudioFile(SoundParameter.pirate + _extension);
                    break;
                case SoundParameter.poutine:
                    _audioService.PlayAudioFile(SoundParameter.poutine + _extension);
                    break;
                case SoundParameter.sadface:
                    _audioService.PlayAudioFile(SoundParameter.sadface + _extension);
                    break;
                case SoundParameter.snoring:
                    _audioService.PlayAudioFile(SoundParameter.snoring + _extension);
                    break;
                case SoundParameter.grinninge:
                    _audioService.PlayAudioFile(SoundParameter.grinninge + _extension);
                    break;
                case SoundParameter.deviltongue:
                    _audioService.PlayAudioFile(SoundParameter.deviltongue + _extension);
                    break;
                case SoundParameter.closedeyes:
                    _audioService.PlayAudioFile(SoundParameter.closedeyes + _extension);
                    break;
                case SoundParameter.laughing:
                    _audioService.PlayAudioFile(SoundParameter.laughing + _extension);
                    break;
                case SoundParameter.grinningy:
                    _audioService.PlayAudioFile(SoundParameter.grinningy + _extension);
                    break;
                case SoundParameter.ninja:
                    _audioService.PlayAudioFile(SoundParameter.ninja + _extension);
                    break;
                case SoundParameter.robotq:
                    _audioService.PlayAudioFile(SoundParameter.robotq + _extension);
                    break;
                case SoundParameter.grinningr:
                    _audioService.PlayAudioFile(SoundParameter.grinningr + _extension);
                    break;
                case SoundParameter.puke:
                    _audioService.PlayAudioFile(SoundParameter.puke + _extension);
                    break;
                case SoundParameter.explodes:
                    _audioService.PlayAudioFile(SoundParameter.explodes + _extension);
                    break;
                case SoundParameter.chilling:
                    _audioService.PlayAudioFile(SoundParameter.chilling + _extension);
                    break;
                case SoundParameter.sleepingd:
                    _audioService.PlayAudioFile(SoundParameter.sleepingd + _extension);
                    break;
                case SoundParameter.zipped:
                    _audioService.PlayAudioFile(SoundParameter.zipped + _extension);
                    break;
                case SoundParameter.death:
                    _audioService.PlayAudioFile(SoundParameter.death + _extension);
                    break;
                case SoundParameter.teeth:
                    _audioService.PlayAudioFile(SoundParameter.teeth + _extension);
                    break;
                case SoundParameter.sweattongue:
                    _audioService.PlayAudioFile(SoundParameter.sweattongue + _extension);
                    break;
                case SoundParameter.kiss:
                    _audioService.PlayAudioFile(SoundParameter.kiss + _extension);
                    break;
                case SoundParameter.hand:
                    _audioService.PlayAudioFile(SoundParameter.hand + _extension);
                    break;
                case SoundParameter.frown:
                    _audioService.PlayAudioFile(SoundParameter.frown + _extension);
                    break;
                case SoundParameter.irritated:
                    _audioService.PlayAudioFile(SoundParameter.irritated + _extension);
                    break;
                case SoundParameter.crying:
                    _audioService.PlayAudioFile(SoundParameter.crying + _extension);
                    break;
                case SoundParameter.devilq:
                    _audioService.PlayAudioFile(SoundParameter.devilq + _extension);
                    break;
                case SoundParameter.hugging:
                    _audioService.PlayAudioFile(SoundParameter.hugging + _extension);
                    break;
                case SoundParameter.yummy:
                    _audioService.PlayAudioFile(SoundParameter.yummy + _extension);
                    break;
                case SoundParameter.tasty:
                    _audioService.PlayAudioFile(SoundParameter.tasty + _extension);
                    break;
                case SoundParameter.airhorn:
                    _audioService.PlayAudioFile(SoundParameter.airhorn + _extension);
                    break;
                case SoundParameter.chicken:
                    _audioService.PlayAudioFile(SoundParameter.chicken + _extension);
                    break;
                case SoundParameter.drum:
                    _audioService.PlayAudioFile(SoundParameter.drum + _extension);
                    break;
                case SoundParameter.fist:
                    _audioService.PlayAudioFile(SoundParameter.fist + _extension);
                    break;
                case SoundParameter.gun:
                    _audioService.PlayAudioFile(SoundParameter.gun + _extension);
                    break;
                case SoundParameter.universal:
                    _audioService.PlayAudioFile(SoundParameter.universal + _extension);
                    break;
            }
        }
    }
}