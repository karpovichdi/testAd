using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using quazimodo.Constants;
using quazimodo.Enums;
using quazimodo.Interfaces;
using Plugin.AudioRecorder;
using quazimodo.Models;
using quazimodo.Services;
using quazimodo.utils;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace quazimodo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Command PositiveMusicBtnClickCommand { get; set; }
        public Command NegativeMusicBtnClickCommand { get; set; }
        public Command NeutralMusicBtnClickCommand { get; set; }
        public Command StopCommand { get; set; }
        public Command HidePopupCommand { get; set; }
        public Command MyAppsCommand { get; set; }
        public Command HideMyAppsCommand { get; set; }
        public Command SelectedMyAppCommand { get; set; }
        public Command RecordCommand { get; set; }
        
        private readonly SmileButtonService _smileButtonService;
        private readonly SoundManagerService _soundManagerService;
        private readonly FirebaseService _firebaseService;
        
        private bool _stopButtonVisible;
        private bool _donationPageVisible;
        private bool _myAppsIsExist;
        private bool _myAppsPageVisible;
        private bool _isPlaying;

        public ObservableRangeCollection<MyApp> MyApps { get; set; }
        public ObservableRangeCollection<CustomSound> CustomSounds { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> NeutralSoundList { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> NegativeSoundList { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> PositiveSoundList { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> PlayingSoundsNow { get; set; }

        public bool AppsIsLoaded { get; set; }

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

        public bool DonationPageVisible
        {
            get => _donationPageVisible;
            set
            {
                if (value)
                {
                    if (App.AdInitialized)
                    {
                        _donationPageVisible = true;
                        OnPropertyChanged(nameof(DonationPageVisible));
                    }

                    return;
                }

                _donationPageVisible = value;
                OnPropertyChanged(nameof(DonationPageVisible));
            }
        }
        
        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                OnPropertyChanged(nameof(IsPlaying));
            }
        }
        
        public MainViewModel()
        {
            PositiveMusicBtnClickCommand = new Command(PositiveClickCommandHandler);
            NegativeMusicBtnClickCommand = new Command(NegativeClickCommandHandler);
            NeutralMusicBtnClickCommand = new Command(NeutralClickCommandHandler);
            StopCommand = new Command(StopCommandHandler);
            HidePopupCommand = new Command(HidePopupCommandHandler);
            MyAppsCommand = new Command(MyAppsCommandHandler);
            HideMyAppsCommand = new Command(HideMyAppsCommandHandler);
            SelectedMyAppCommand = new Command(SelectedMyAppCommandHandler);
            RecordCommand = new Command(RecordCommandHandler);
            
            MyApps = new ObservableRangeCollection<MyApp>();
            CustomSounds = new ObservableRangeCollection<CustomSound>();
            NeutralSoundList = new ObservableRangeCollection<ButtonSmileViewModel>();
            PositiveSoundList = new ObservableRangeCollection<ButtonSmileViewModel>();
            NegativeSoundList = new ObservableRangeCollection<ButtonSmileViewModel>();
            PlayingSoundsNow = new ObservableRangeCollection<ButtonSmileViewModel>();
            
            _smileButtonService = new SmileButtonService();
            _soundManagerService = new SoundManagerService();
            _firebaseService = new FirebaseService();
            
            FillSoundListByType(SmileType.Positive);
            FillSoundListByType(SmileType.Neutral);
            FillSoundListByType(SmileType.Negative);

            // record testing
            FillCustomSounds();

            _firebaseService.Initialize();

            Connectivity.ConnectivityChanged += ConnectivityChangedHandler;
            PlayingSoundsNow.CollectionChanged += PlayingSoundsChanged;

            MessagingCenter.Instance.Subscribe<byte[]>(this, MessagingCenterConstants.LastSongFinished,
                (array) => LastSoundStopped());
        }

        private void PlayingSoundsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var changedAction = e.Action;
            switch (changedAction)
            {
                case NotifyCollectionChangedAction.Add:
                    var newItems = e.NewItems;
                    foreach (var item in newItems)
                    {
                        if (item is ButtonSmileViewModel viewModel) viewModel.IsPlaying = true;
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var oldItems = e.OldItems;
                    foreach (var item in oldItems)
                    {
                        if (item is ButtonSmileViewModel viewModel) RequestToDisableSmile(viewModel);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        private void RequestToDisableSmile(ButtonSmileViewModel viewModel)
        {
            var viewModels = PlayingSoundsNow.Where(x => 
                x.CommandParameter == viewModel.CommandParameter);

            if (!viewModels.Any()) viewModel.IsPlaying = false;
        }
        
        private async void NeutralClickCommandHandler(object obj)
        {
            try
            {
                if (obj is string) return;
                await PrepareToPlaySound();

                var soundParameter = (SoundParameter) obj;
                AddNewPlayingSound(soundParameter, NeutralSoundList);
                _soundManagerService.PlaySound(soundParameter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AddNewPlayingSound(SoundParameter param, ObservableRangeCollection<ButtonSmileViewModel> list)
        {
            var smileData = list.First(x => x.CommandParameter == param);
            PlayingSoundsNow.Add(smileData);
        }
        
        private async void NegativeClickCommandHandler(object obj)
        {
            try
            {
                if (obj is string) return;
                await PrepareToPlaySound();
                var soundParameter = (SoundParameter) obj;
                AddNewPlayingSound(soundParameter, NegativeSoundList);
                _soundManagerService.PlaySound(soundParameter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void PositiveClickCommandHandler(object obj)
        {
            try
            {
                if (obj is string) return;

                await PrepareToPlaySound();
                _soundManagerService.PlaySound((SoundParameter) obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }    
        }

        private void RecordCommandHandler()
        {
            _soundManagerService.Record();
        }
        
        private void MyAppsCommandHandler()
        {
            MyAppsPageVisible = true;
            DonationPageVisible = false;
        }

        private void HideMyAppsCommandHandler()
        {
            MyAppsPageVisible = false;
        }

        private void HidePopupCommandHandler()
        {
            DonationPageVisible = false;
        }
        
        private async void StopCommandHandler(object obj)
        {
            await _soundManagerService.StopPlaying();
            PlayingSoundsNow.ForEach(x => x.IsPlaying = false);
            StopButtonVisible = false;
        }
        
        private void SelectedMyAppCommandHandler(object obj)
        {
            try
            {
                if (obj is string downloadUrl)  Launcher.OpenAsync(new Uri("market://details?id=" + downloadUrl));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task FillSoundListByType(SmileType smileType)
        {
            switch (smileType)
            {
                case SmileType.Positive:
                    PositiveSoundList.AddRange(await _smileButtonService.GetPositiveModels());
                    break;
                case SmileType.Negative:
                    NegativeSoundList.AddRange(await _smileButtonService.GetNegativeModels());
                    break;
                case SmileType.Neutral:
                    NeutralSoundList.AddRange(await _smileButtonService.GetNeutralModels());
                    break;
            }
        }

        private void FillCustomSounds()
        {
            var customSounds = new List<CustomSound>();
            var innerCounter = 0;
            for (var i = 1; i < 31; i++)
            {
                if (i > 10)
                {
                    innerCounter = 1;
                }

                var customSound = new CustomSound
                {
                    Number = i,
                    Label = innerCounter.ToString(),
                    Visible = i == 1 || i == 11 || i == 21,
                    Path = ""
                };
                innerCounter++;
                customSounds.Add(customSound);
            }

            CustomSounds.AddRange(customSounds);
        }

        public async Task FillMyAppList()
        {
            if (!MyApps.Any())
            {
                var myApps = await GetMyApps();
                MyAppsIsExist = myApps.Any();

                foreach (var app in myApps)
                {
                    MyApps.Add(app);
                }

                AppsIsLoaded = true;
            }
        }

        public async Task<List<MyApp>> GetMyApps()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet) return await _firebaseService.GetListApps();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
        
        private void LastSoundStopped()
        {
            StopButtonVisible = false;
        }
        
        private async Task PrepareToPlaySound()
        {
            if (!_soundManagerService.MicrophonePermissionsGranted) await _soundManagerService.CheckPermissions();
                
            StopButtonVisible = true;
            App.CountOfPlayedSound++;

            if (App.CountOfPlayedSound >= AdConstants.ClicksCountBeforeAd)
            {
                DonationPageVisible = true;
                MyAppsPageVisible = false;
                App.CountOfPlayedSound = 0;
            }
        }

        void ConnectivityChangedHandler(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.ConnectionProfiles != null)
            {
                var connectionProfiles = e.ConnectionProfiles.ToList();
                if (connectionProfiles.Contains(ConnectionProfile.WiFi) ||
                    connectionProfiles.Contains(ConnectionProfile.Cellular)) FillMyAppList();
            }
        }
    }
}