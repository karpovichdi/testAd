using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using quazimodo.Models;
using quazimodo.Models.Enums;
using quazimodo.Resources;
using quazimodo.Services;
using quazimodo.Services.Interfaces;
using quazimodo.Utilities;
using quazimodo.Utilities.Constants;
using quazimodo.Views.Popups;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace quazimodo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly SmileButtonSourceService _smileButtonSourceService;
        private readonly ISoundService _soundService;
        private readonly FirebaseService _firebaseService;

        private bool _stopButtonVisible;
        private bool _donationPageVisible;
        private bool _myAppsIsExist;
        private bool _myAppsPageVisible;
        private bool _tooMuchSoundsInOneTime;
        private bool _recordingViewVisible;
        private bool _microphoneIsDisabledByUser;
        private bool _confirmPopupVisible;        
        private bool _stopRecordingPopupVisible;
        private bool _admpPopupVisible;
        private double _recordingViewProgress;
        private string _confirmMessage;
        private string _confirmPopupPositiveBtnText;
        private string _confirmPopupNegativeBtnText;
        private Timer _timer;

        #endregion

        #region Properties

        public bool AppsIsLoaded { get; set; }
        public Command SoundBtnClickCommand { get; set; }
        public Command StopCommand { get; set; }
        public Command HidePopupCommand { get; set; }
        public Command MyAppsCommand { get; set; }
        public Command HideMyAppsCommand { get; set; }
        public Command SelectedMyAppCommand { get; set; }
        public Command StopRecordingCommand { get; set; }
        public Command HideADMPPopusCommmand { get; set; }
        public Command HideStopRecordingPopupCommand { get; set; }
        public ObservableRangeCollection<MyApp> MyApps { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> SmileSoundList { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> PlayingSoundsNow { get; set; }

        #endregion

        #region BindableProperties

        public bool MicrophoneIsDisabledByUser
        {
            get => _microphoneIsDisabledByUser;
            set
            {
                _microphoneIsDisabledByUser = value;
                if (value) HideAllRecordButtons();
            }
        }

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

        public bool RecordingViewVisible
        {
            get => _recordingViewVisible;
            set
            {
                _recordingViewVisible = value;
                OnPropertyChanged(nameof(RecordingViewVisible));
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
        
        public bool StopRecordingPopupVisible
        {
            get => _stopRecordingPopupVisible;
            set
            {
                _stopRecordingPopupVisible = value;
                OnPropertyChanged(nameof(StopRecordingPopupVisible));
            }
        }
        
        public bool ADMPPopupVisible
        {
            get => _admpPopupVisible;
            set
            {
                _admpPopupVisible = value;
                OnPropertyChanged(nameof(ADMPPopupVisible));
            }
        }

        public double RecordingViewProgress
        {
            get => _recordingViewProgress;
            set
            {
                _recordingViewProgress = value;
                OnPropertyChanged(nameof(RecordingViewProgress));
            }
        }

        #endregion

        public MainViewModel()
        {
            SoundBtnClickCommand = new Command(SoundSelectedHandler);
            StopCommand = new Command(StopCommandHandler);
            HidePopupCommand = new Command(HidePopupCommandHandler);
            MyAppsCommand = new Command(MyAppsCommandHandler);
            HideMyAppsCommand = new Command(HideMyAppsCommandHandler);
            SelectedMyAppCommand = new Command(SelectedMyAppCommandHandler);
            StopRecordingCommand = new Command(StopRecordingCommandHandler);
            HideADMPPopusCommmand = new Command(HideADMPPopusCommmandHandler);
            HideStopRecordingPopupCommand = new Command(HideStopRecordingPopupCommandHandler);

            MyApps = new ObservableRangeCollection<MyApp>();
            SmileSoundList = new ObservableRangeCollection<ButtonSmileViewModel>();
            PlayingSoundsNow = new ObservableRangeCollection<ButtonSmileViewModel>();

            _smileButtonSourceService = new SmileButtonSourceService();
            _firebaseService = new FirebaseService();
            _soundService = DependencyService.Get<ISoundService>();

            _firebaseService.Initialize();

            SmileSoundList.AddRange(_smileButtonSourceService.GetSmiles());

            Connectivity.ConnectivityChanged += ConnectivityChangedHandler;
            PlayingSoundsNow.CollectionChanged += PlayingSoundsChanged;

            _soundService.SoundReleased += SoundReleasedHandler;
            _soundService.RecordReleased += RecordReleasedHandler;
            _soundService.AppClosed += AppClosedHandler;
        }

        #region Methods
        
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
            if (PlayingSoundsNow.Count < ConstantsForms.MaxCountOfSoundInOneTime) TooMuchSoundsInOneTime = false;
        }

        private void RequestToDisableSmile(ButtonSmileViewModel viewModel)
        {
            var viewModels = PlayingSoundsNow.Where(x =>
                x.CommandParameter == viewModel.CommandParameter);

            if (!viewModels.Any()) viewModel.IsPlaying = false;
        }

        private async Task PrepareToRecording(ButtonSmileViewModel buttonSmileViewModel)
        {
            await StopSounds();

            RecordingViewVisible = true;
            
            _timer = new Timer(500) {AutoReset = true};
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
            
            await _soundService.StartRecording(buttonSmileViewModel.CommandParameter);
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (RecordingViewProgress < 100)
            {
                RecordingViewProgress += ConstantsForms.AdditionalValueToRecordProgress ;
            }
        }

        public async Task StopRecording(bool closeWithoutPopup = false)
        {
            if (closeWithoutPopup)
            {
            }
            else
            {
                StopRecordingPopupVisible = true;
            }
            
            await _soundService.StopRecording();
            StopRecordingOnUi();
        }


        private void StopRecordingOnUi()
        {
            RecordingViewVisible = false;
            RecordingViewProgress = 0;
            
            _timer.Stop();
            _timer = null;
        }

        private async Task StopSounds()
        {
            await _soundService.StopPlayingAll();
            PlayingSoundsNow.ForEach(x => x.IsPlaying = false);
            PlayingSoundsNow.Clear();
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

        private async Task PrepareToPlaySound(ButtonSmileViewModel smileViewModel)
        {
            StopButtonVisible = true;

            App.CountOfPlayedSound++;

            if (App.CountOfPlayedSound >= AdConstants.ClicksCountBeforeAd)
            {
                DonationPageVisible = true;
                MyAppsPageVisible = false;
                App.CountOfPlayedSound = 0;
            }

            PlayingSoundsNow.Add(smileViewModel);
            _soundService.CreateSoundPathAndPlay(smileViewModel.CommandParameter);
        }

        private void HideAllRecordButtons()
        {
            var smileViewModels = SmileSoundList.Where(x => x.IsRecord);
            foreach (var viewModel in smileViewModels)
            {
                viewModel.IsRecord = false;
                viewModel.IsVisibleRecord = false;
            }
        }

        #endregion
        
        #region Handlers
        
        private void HideStopRecordingPopupCommandHandler(object obj)
        {
            var param = obj as string;
            if (param == ConstantsForms.Positive)
            {
                // DO SAVE
            }

            StopRecordingPopupVisible = false;
            RecordingViewVisible = false;
            ADMPPopupVisible = true;
        }

        private void SoundReleasedHandler(SoundParameter parameter)
        {
            var model = PlayingSoundsNow.FirstOrDefault(x => x.CommandParameter == parameter);
            if (model != null) PlayingSoundsNow.Remove(model);
        }

        private void RecordReleasedHandler()
        {
            StopRecordingOnUi();
        }
        
        private async void SoundSelectedHandler(object obj)
        {
            try
            {
                if (obj is string) return;
                var soundParameter = (SoundParameter) obj;

                if (!MicrophoneIsDisabledByUser)
                {
                    var permissions = await _soundService.CheckPermissions();
                    if (permissions != PermissionStatus.Granted) ADMPPopupVisible = true;
                }

                var smileViewModel = SmileSoundList.FirstOrDefault(x => x.CommandParameter == soundParameter);
                if (smileViewModel != null && smileViewModel.IsPlusButton)
                {
                    await PrepareToRecording(smileViewModel);
                }
                else
                {
                    if (PlayingSoundsNow.Count < ConstantsForms.MaxCountOfSoundInOneTime)
                    {
                        await PrepareToPlaySound(smileViewModel);
                    }
                    else
                    {
                        TooMuchSoundsInOneTime = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void SelectedMyAppCommandHandler(object obj)
        {
            try
            {
                if (obj is string downloadUrl) Launcher.OpenAsync(new Uri("market://details?id=" + downloadUrl));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private async void HideADMPPopusCommmandHandler(object obj)
        {
            var param = (string) obj;

            if (param == ConstantsForms.Positive)
            {
                MicrophoneIsDisabledByUser = true;
            }
            else
            {
                MicrophoneIsDisabledByUser = false;
                ADMPPopupVisible = false;
                var permissionStatus = await _soundService.CheckPermissions();
                if (permissionStatus != PermissionStatus.Granted) MicrophoneIsDisabledByUser = true;
            }
        }

        private void StopRecordingCommandHandler()
        {
            StopRecording();
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
            StopRecording(true);
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
        
        #endregion
    }
}