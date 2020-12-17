using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using quazimodo.Constants;
using quazimodo.Enums;
using quazimodo.Interfaces;
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
        public Command SoundBtnClickCommand { get; set; }
        public Command StopCommand { get; set; }
        public Command HidePopupCommand { get; set; }
        public Command MyAppsCommand { get; set; }
        public Command HideMyAppsCommand { get; set; }
        public Command SelectedMyAppCommand { get; set; }
        public Command RecordCommand { get; set; }
        
        private readonly SmileButtonSourceService _smileButtonSourceService;
        private readonly ISoundService _soundService;
        private readonly FirebaseService _firebaseService;
        
        private bool _stopButtonVisible;
        private bool _donationPageVisible;
        private bool _myAppsIsExist;
        private bool _myAppsPageVisible;
        private bool _tooMuchSoundsInOneTime;

        public ObservableRangeCollection<MyApp> MyApps { get; set; }
        public ObservableRangeCollection<CustomSound> CustomSounds { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> SmileSoundList { get; set; }
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
        
        public bool TooMuchSoundsInOneTime
        {
            get => _tooMuchSoundsInOneTime;
            set
            {
                _tooMuchSoundsInOneTime = value;
                OnPropertyChanged(nameof(TooMuchSoundsInOneTime));
            }
        }
        
        public MainViewModel()
        {
            SoundBtnClickCommand = new Command(SoundSelectedHandler);
            StopCommand = new Command(StopCommandHandler);
            HidePopupCommand = new Command(HidePopupCommandHandler);
            MyAppsCommand = new Command(MyAppsCommandHandler);
            HideMyAppsCommand = new Command(HideMyAppsCommandHandler);
            SelectedMyAppCommand = new Command(SelectedMyAppCommandHandler);
            RecordCommand = new Command(RecordCommandHandler);
            
            MyApps = new ObservableRangeCollection<MyApp>();
            CustomSounds = new ObservableRangeCollection<CustomSound>();
            SmileSoundList = new ObservableRangeCollection<ButtonSmileViewModel>();
            PlayingSoundsNow = new ObservableRangeCollection<ButtonSmileViewModel>();
            
            _smileButtonSourceService = new SmileButtonSourceService();
            _firebaseService = new FirebaseService();
            _soundService = DependencyService.Get<ISoundService>();
            
            _firebaseService.Initialize();

            SmileSoundList.AddRange(_smileButtonSourceService.GetSmiles());

            Connectivity.ConnectivityChanged += ConnectivityChangedHandler;
            PlayingSoundsNow.CollectionChanged += PlayingSoundsChanged;

            _soundService.SoundReleased += SoundReleased;
            _soundService.AppClosed += AppClosedHandler;
        }

        private void SoundReleased(SoundParameter parameter)
        {
            var model = PlayingSoundsNow.FirstOrDefault(x => x.CommandParameter == parameter);
            if(model != null) PlayingSoundsNow.Remove(model);
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
                    RequestToDisableTooMuchPopup();
                    foreach (var item in oldItems)
                    {
                        if (item is ButtonSmileViewModel viewModel) RequestToDisableSmile(viewModel);
                    }

                    if (PlayingSoundsNow.Count == 0) StopButtonVisible = false;
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    StopButtonVisible = false;
                    break;
            }
        }

        private void RequestToDisableTooMuchPopup()
        {
            if (PlayingSoundsNow.Count() < ConstantsForms.MaxCountOfSoundInOneTime) TooMuchSoundsInOneTime = false;
        }

        private void RequestToDisableSmile(ButtonSmileViewModel viewModel)
        {
            var viewModels = PlayingSoundsNow.Where(x => 
                x.CommandParameter == viewModel.CommandParameter);

            if (!viewModels.Any()) viewModel.IsPlaying = false;
        }
        
        private async void SoundSelectedHandler(object obj)
        {
            try
            {
                if (obj is string) return;
                var soundParameter = (SoundParameter) obj;

                if (PlayingSoundsNow.Count < ConstantsForms.MaxCountOfSoundInOneTime)
                {
                    await PrepareToPlaySound();
                    AddNewPlayingSound(soundParameter, SmileSoundList);
                    _soundService.CreateSoundPathAndPlay(soundParameter);
                }
                else
                {
                    TooMuchSoundsInOneTime = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AddNewPlayingSound(SoundParameter param, ObservableRangeCollection<ButtonSmileViewModel> list)
        {
            var smileData = list.FirstOrDefault(x => x.CommandParameter == param);
            if (smileData != null) PlayingSoundsNow.Add(smileData);
        }
        
        private void RecordCommandHandler()
        {
            //_soundService.Record();
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
            StopSounds();
        }

        private void AppClosedHandler()
        {
            StopSounds();
        }

        private async Task StopSounds()
        {
            await _soundService.StopPlayingAll();
            PlayingSoundsNow.ForEach(x => x.IsPlaying = false);
            PlayingSoundsNow.Clear();
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
            if (!_soundService.MicrophonePermissionsGranted) await _soundService.CheckPermissions();
                
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