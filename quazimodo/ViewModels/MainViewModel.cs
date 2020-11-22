using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using quazimodo.Constants;
using quazimodo.Enums;
using quazimodo.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using quazimodo.Models;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace quazimodo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Command MusicBtnClickCommand { get; set; }
        public Command StopCommand { get; set; }
        public Command HidePopupCommand { get; set; }
        public Command MyAppsCommand { get; set; }
        public Command HideMyAppsCommand { get; set; }
        
        public Command  SelectedMyAppCommand { get; set; }
        
        private const string Extension = ".mp3";

        private readonly IAudioService _audioService;
        private readonly FirebaseClient _firebaseClient;
        
        private bool _stopButtonVisible;
        private bool _popupVisible;
        private bool _myAppsIsExist;
        private bool _myAppsPageVisible;

        public ObservableCollection<MyApp> MyApps { get; set; }
        
        public bool MyAppsPageVisible
        {
            get => _myAppsPageVisible;
            set
            {
                _myAppsPageVisible = value;
                OnPropertyChanged(nameof(MyAppsPageVisible));
            }
        }
        
        public bool MyAppsIsExist
        {
            get => _myAppsIsExist;
            set
            {
                _myAppsIsExist = value;
                OnPropertyChanged(nameof(MyAppsIsExist));
            }
        }

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
            MyAppsCommand = new Command(MyAppsCommandHandler);
            HideMyAppsCommand = new Command(HideMyAppsCommandHandler);
            SelectedMyAppCommand = new Command(SelectedMyAppCommandHandler);

            MyApps = new ObservableCollection<MyApp>();

            _audioService = DependencyService.Get<IAudioService>();

            _firebaseClient = new FirebaseClient(FirebaseConstants.DatabaseUrl);

            Connectivity.ConnectivityChanged += ConnectivityChangedHandler;

            MessagingCenter.Instance.Subscribe<byte[]>(this, MessagingCenterConstants.LastSongFinished,
                (array) => LastSoundStopped());
        }

        public async Task FillMyAppList()
        {
            if (!MyApps.Any())
            {
                try
                {
                    var myApps = await GetMyApps();

                    MyAppsIsExist = myApps.Any();

                    foreach (var app in myApps)
                    {
                        MyApps.Add(app);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public async Task<List<MyApp>> GetMyApps()
        {
            var current = Connectivity.NetworkAccess;

            var locale = FirebaseConstants.LocalizationEn;
            
            if (App.TwoLetterIsoLanguageName.Contains(FirebaseConstants.LocalizationRu))
            {
                locale = FirebaseConstants.LocalizationRu;
            }

            if (current == NetworkAccess.Internet)
            {
                return (await _firebaseClient
                    .Child(FirebaseConstants.MyAppsEndpoint).Child(locale)
                    .OnceAsync<MyApp>()).Select(item => new MyApp
                {
                    Name = item.Object.Name,
                    Description = item.Object.Description,
                    ImageUrl = item.Object.ImageUrl,
                    DownloadUrl = item.Object.DownloadUrl
                }).ToList();
            }

            return null;
        }

        void ConnectivityChangedHandler(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;

            if (profiles != null)
            {
                var connectionProfiles = profiles.ToList();

                if (connectionProfiles.Contains(ConnectionProfile.WiFi) ||
                    connectionProfiles.Contains(ConnectionProfile.Cellular))
                {
                    FillMyAppList();
                }
            }
        }

        private void SelectedMyAppCommandHandler(object obj)
        {
            if (obj is string downloadUrl)
            {
                Launcher.OpenAsync(new Uri("market://details?id=" + downloadUrl));
            }
        }
        
        private void MyAppsCommandHandler()
        {
            MyAppsPageVisible = true;
            PopupVisible = false;
        }
        
        private void HideMyAppsCommandHandler()
        {
            MyAppsPageVisible = false;
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
            try
            {
                if (obj is string) return;
                var parameter = (SoundParameter) obj;

                StopButtonVisible = true;
                App.CountOfPlayedSound++;

                if (App.CountOfPlayedSound >= AdConstants.ClicksCountBeforeAd)
                {
                    PopupVisible = true;
                    MyAppsPageVisible = false;
                    App.CountOfPlayedSound = 0;
                }

                switch (parameter)
                {
                    case SoundParameter.Reject:
                        _audioService.PlayAudioFile(SoundParameter.Reject + Extension);
                        break;
                    case SoundParameter.Accept:
                        _audioService.PlayAudioFile(SoundParameter.Accept + Extension);
                        break;
                    case SoundParameter.Love:
                        _audioService.PlayAudioFile(SoundParameter.Love + Extension);
                        break;
                    case SoundParameter.Crank:
                        _audioService.PlayAudioFile(SoundParameter.Crank + Extension);
                        break;
                    case SoundParameter.Creepypasta:
                        _audioService.PlayAudioFile(SoundParameter.Creepypasta + Extension);
                        break;
                    case SoundParameter.Wow:
                        _audioService.PlayAudioFile(SoundParameter.Wow + Extension);
                        break;
                    case SoundParameter.Star:
                        _audioService.PlayAudioFile(SoundParameter.Star + Extension);
                        break;
                    case SoundParameter.wink:
                        _audioService.PlayAudioFile(SoundParameter.wink + Extension);
                        break;
                    case SoundParameter.thinking:
                        _audioService.PlayAudioFile(SoundParameter.thinking + Extension);
                        break;
                    case SoundParameter.confusingq:
                        _audioService.PlayAudioFile(SoundParameter.confusingq + Extension);
                        break;
                    case SoundParameter.blowkiss:
                        _audioService.PlayAudioFile(SoundParameter.blowkiss + Extension);
                        break;
                    case SoundParameter.grandfather:
                        _audioService.PlayAudioFile(SoundParameter.grandfather + Extension);
                        break;
                    case SoundParameter.sweating:
                        _audioService.PlayAudioFile(SoundParameter.sweating + Extension);
                        break;
                    case SoundParameter.laughingq:
                        _audioService.PlayAudioFile(SoundParameter.laughingq + Extension);
                        break;
                    case SoundParameter.neutral:
                        _audioService.PlayAudioFile(SoundParameter.neutral + Extension);
                        break;
                    case SoundParameter.grinningq:
                        _audioService.PlayAudioFile(SoundParameter.grinningq + Extension);
                        break;
                    case SoundParameter.skull:
                        _audioService.PlayAudioFile(SoundParameter.skull + Extension);
                        break;
                    case SoundParameter.sneezing:
                        _audioService.PlayAudioFile(SoundParameter.sneezing + Extension);
                        break;
                    case SoundParameter.flushed:
                        _audioService.PlayAudioFile(SoundParameter.flushed + Extension);
                        break;
                    case SoundParameter.joy:
                        _audioService.PlayAudioFile(SoundParameter.joy + Extension);
                        break;
                    case SoundParameter.silentq:
                        _audioService.PlayAudioFile(SoundParameter.silentq + Extension);
                        break;
                    case SoundParameter.sad:
                        _audioService.PlayAudioFile(SoundParameter.sad + Extension);
                        break;
                    case SoundParameter.tears:
                        _audioService.PlayAudioFile(SoundParameter.tears + Extension);
                        break;
                    case SoundParameter.amazed:
                        _audioService.PlayAudioFile(SoundParameter.amazed + Extension);
                        break;
                    case SoundParameter.lyingq:
                        _audioService.PlayAudioFile(SoundParameter.lyingq + Extension);
                        break;
                    case SoundParameter.smilingface:
                        _audioService.PlayAudioFile(SoundParameter.smilingface + Extension);
                        break;
                    case SoundParameter.teasing:
                        _audioService.PlayAudioFile(SoundParameter.teasing + Extension);
                        break;
                    case SoundParameter.surprised:
                        _audioService.PlayAudioFile(SoundParameter.surprised + Extension);
                        break;
                    case SoundParameter.dizzy:
                        _audioService.PlayAudioFile(SoundParameter.dizzy + Extension);
                        break;
                    case SoundParameter.angry:
                        _audioService.PlayAudioFile(SoundParameter.angry + Extension);
                        break;
                    case SoundParameter.mouth:
                        _audioService.PlayAudioFile(SoundParameter.mouth + Extension);
                        break;
                    case SoundParameter.alien:
                        _audioService.PlayAudioFile(SoundParameter.alien + Extension);
                        break;
                    case SoundParameter.zanyq:
                        _audioService.PlayAudioFile(SoundParameter.zanyq + Extension);
                        break;
                    case SoundParameter.smile:
                        _audioService.PlayAudioFile(SoundParameter.smile + Extension);
                        break;
                    case SoundParameter.sunglassesq:
                        _audioService.PlayAudioFile(SoundParameter.sunglassesq + Extension);
                        break;
                    case SoundParameter.poop:
                        _audioService.PlayAudioFile(SoundParameter.poop + Extension);
                        break;
                    case SoundParameter.flatmouth:
                        _audioService.PlayAudioFile(SoundParameter.flatmouth + Extension);
                        break;
                    case SoundParameter.eyepatch:
                        _audioService.PlayAudioFile(SoundParameter.eyepatch + Extension);
                        break;
                    case SoundParameter.Laughterq:
                        _audioService.PlayAudioFile(SoundParameter.Laughterq + Extension);
                        break;
                    case SoundParameter.money:
                        _audioService.PlayAudioFile(SoundParameter.money + Extension);
                        break;
                    case SoundParameter.dollar:
                        _audioService.PlayAudioFile(SoundParameter.dollar + Extension);
                        break;
                    case SoundParameter.upset:
                        _audioService.PlayAudioFile(SoundParameter.upset + Extension);
                        break;
                    case SoundParameter.grimacing:
                        _audioService.PlayAudioFile(SoundParameter.grimacing + Extension);
                        break;
                    case SoundParameter.cryd:
                        _audioService.PlayAudioFile(SoundParameter.cryd + Extension);
                        break;
                    case SoundParameter.lol:
                        _audioService.PlayAudioFile(SoundParameter.lol + Extension);
                        break;
                    case SoundParameter.sunglassesqq:
                        _audioService.PlayAudioFile(SoundParameter.sunglassesqq + Extension);
                        break;
                    case SoundParameter.mouthfull:
                        _audioService.PlayAudioFile(SoundParameter.mouthfull + Extension);
                        break;
                    case SoundParameter.edevil:
                        _audioService.PlayAudioFile(SoundParameter.edevil + Extension);
                        break;
                    case SoundParameter.angryface:
                        _audioService.PlayAudioFile(SoundParameter.angryface + Extension);
                        break;
                    case SoundParameter.sleeping:
                        _audioService.PlayAudioFile(SoundParameter.sleeping + Extension);
                        break;
                    case SoundParameter.tongueout:
                        _audioService.PlayAudioFile(SoundParameter.tongueout + Extension);
                        break;
                    case SoundParameter.Laughter:
                        _audioService.PlayAudioFile(SoundParameter.Laughter + Extension);
                        break;
                    case SoundParameter.angel:
                        _audioService.PlayAudioFile(SoundParameter.angel + Extension);
                        break;
                    case SoundParameter.policeman:
                        _audioService.PlayAudioFile(SoundParameter.policeman + Extension);
                        break;
                    case SoundParameter.thief:
                        _audioService.PlayAudioFile(SoundParameter.thief + Extension);
                        break;
                    case SoundParameter.devilsmile:
                        _audioService.PlayAudioFile(SoundParameter.devilsmile + Extension);
                        break;
                    case SoundParameter.disturb:
                        _audioService.PlayAudioFile(SoundParameter.disturb + Extension);
                        break;
                    case SoundParameter.medicalmask:
                        _audioService.PlayAudioFile(SoundParameter.medicalmask + Extension);
                        break;
                    case SoundParameter.expressionless:
                        _audioService.PlayAudioFile(SoundParameter.expressionless + Extension);
                        break;
                    case SoundParameter.pirate:
                        _audioService.PlayAudioFile(SoundParameter.pirate + Extension);
                        break;
                    case SoundParameter.poutine:
                        _audioService.PlayAudioFile(SoundParameter.poutine + Extension);
                        break;
                    case SoundParameter.sadface:
                        _audioService.PlayAudioFile(SoundParameter.sadface + Extension);
                        break;
                    case SoundParameter.snoring:
                        _audioService.PlayAudioFile(SoundParameter.snoring + Extension);
                        break;
                    case SoundParameter.grinninge:
                        _audioService.PlayAudioFile(SoundParameter.grinninge + Extension);
                        break;
                    case SoundParameter.deviltongue:
                        _audioService.PlayAudioFile(SoundParameter.deviltongue + Extension);
                        break;
                    case SoundParameter.closedeyes:
                        _audioService.PlayAudioFile(SoundParameter.closedeyes + Extension);
                        break;
                    case SoundParameter.laughing:
                        _audioService.PlayAudioFile(SoundParameter.laughing + Extension);
                        break;
                    case SoundParameter.grinningy:
                        _audioService.PlayAudioFile(SoundParameter.grinningy + Extension);
                        break;
                    case SoundParameter.ninja:
                        _audioService.PlayAudioFile(SoundParameter.ninja + Extension);
                        break;
                    case SoundParameter.robotq:
                        _audioService.PlayAudioFile(SoundParameter.robotq + Extension);
                        break;
                    case SoundParameter.grinningr:
                        _audioService.PlayAudioFile(SoundParameter.grinningr + Extension);
                        break;
                    case SoundParameter.puke:
                        _audioService.PlayAudioFile(SoundParameter.puke + Extension);
                        break;
                    case SoundParameter.explodes:
                        _audioService.PlayAudioFile(SoundParameter.explodes + Extension);
                        break;
                    case SoundParameter.chilling:
                        _audioService.PlayAudioFile(SoundParameter.chilling + Extension);
                        break;
                    case SoundParameter.sleepingd:
                        _audioService.PlayAudioFile(SoundParameter.sleepingd + Extension);
                        break;
                    case SoundParameter.zipped:
                        _audioService.PlayAudioFile(SoundParameter.zipped + Extension);
                        break;
                    case SoundParameter.death:
                        _audioService.PlayAudioFile(SoundParameter.death + Extension);
                        break;
                    case SoundParameter.teeth:
                        _audioService.PlayAudioFile(SoundParameter.teeth + Extension);
                        break;
                    case SoundParameter.sweattongue:
                        _audioService.PlayAudioFile(SoundParameter.sweattongue + Extension);
                        break;
                    case SoundParameter.kiss:
                        _audioService.PlayAudioFile(SoundParameter.kiss + Extension);
                        break;
                    case SoundParameter.hand:
                        _audioService.PlayAudioFile(SoundParameter.hand + Extension);
                        break;
                    case SoundParameter.frown:
                        _audioService.PlayAudioFile(SoundParameter.frown + Extension);
                        break;
                    case SoundParameter.irritated:
                        _audioService.PlayAudioFile(SoundParameter.irritated + Extension);
                        break;
                    case SoundParameter.crying:
                        _audioService.PlayAudioFile(SoundParameter.crying + Extension);
                        break;
                    case SoundParameter.devilq:
                        _audioService.PlayAudioFile(SoundParameter.devilq + Extension);
                        break;
                    case SoundParameter.hugging:
                        _audioService.PlayAudioFile(SoundParameter.hugging + Extension);
                        break;
                    case SoundParameter.yummy:
                        _audioService.PlayAudioFile(SoundParameter.yummy + Extension);
                        break;
                    case SoundParameter.tasty:
                        _audioService.PlayAudioFile(SoundParameter.tasty + Extension);
                        break;
                    case SoundParameter.airhorn:
                        _audioService.PlayAudioFile(SoundParameter.airhorn + Extension);
                        break;
                    case SoundParameter.chicken:
                        _audioService.PlayAudioFile(SoundParameter.chicken + Extension);
                        break;
                    case SoundParameter.drum:
                        _audioService.PlayAudioFile(SoundParameter.drum + Extension);
                        break;
                    case SoundParameter.fist:
                        _audioService.PlayAudioFile(SoundParameter.fist + Extension);
                        break;
                    case SoundParameter.gun:
                        _audioService.PlayAudioFile(SoundParameter.gun + Extension);
                        break;
                    case SoundParameter.universal:
                        _audioService.PlayAudioFile(SoundParameter.universal + Extension);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}