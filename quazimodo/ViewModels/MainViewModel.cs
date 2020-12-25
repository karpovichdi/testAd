using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using quazimodo.Models;
using quazimodo.Models.Enums;
using quazimodo.Services;
using quazimodo.Services.Interfaces;
using quazimodo.Utilities;
using quazimodo.Utilities.Constants;
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
        private bool _recordViewVisible;
        private bool _microphoneIsDisabledByUser;
        private bool _stopRecordingPopupVisible;
        private bool _deleteRecordsMode;
        private bool _admpPopupVisible;
        private bool _topButtonVisible;
        private double _recordingViewProgress;
        private string _topGlyph = FontIcons.cog;
        private Timer _recordProgressTimer;
        private ButtonSmileViewModel _lastClickedViewModel;

        #endregion

        #region Properties

        public bool AppsIsLoaded { get; set; }
        public SmileItemSourceViewModel ViewModelItemSource { get; set; }
        public Command SongClickCommand { get; set; }
        public Command SettingsBtnClickedCommand { get; set; }
        public Command StopCommand { get; set; }
        public Command HideDonationPageCommand { get; set; }
        public Command ShowMyAppListCommand { get; set; }
        public Command HideMyAppsCommand { get; set; }
        public Command MyAppSelectedCommand { get; set; }
        public Command StopRecordCommand { get; set; }
        public Command HideADMPPopupCommmand { get; set; }
        public Command HideRecordPopupCommand { get; set; }
        public ObservableRangeCollection<MyApp> MyApps { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> PlayingSong { get; set; }

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

        public bool RecordViewVisible
        {
            get => _recordViewVisible;
            set
            {
                _recordViewVisible = value;
                OnPropertyChanged(nameof(RecordViewVisible));
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
        
        public bool TopButtonVisible
        {
            get => _topButtonVisible;
            set
            {
                _topButtonVisible = value;
                OnPropertyChanged(nameof(TopButtonVisible));
            }
        }
        
        public bool DeleteRecordsMode
        {
            get => _deleteRecordsMode;
            set
            {
                _deleteRecordsMode = value;
                
                if (value)
                {
                    TopGlyph = FontIcons.cross;
                    ViewModelItemSource.ItemSource.ForEach(x => x.TurnOnDeleteMode());
                }
                else
                {
                    TopGlyph = FontIcons.cog;
                    ViewModelItemSource.ItemSource.ForEach(x => x.TurnOffDeleteMode());
                }

                OnPropertyChanged(nameof(DeleteRecordsMode));
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
        
        public string TopGlyph
        {
            get => _topGlyph;
            set
            {
                _topGlyph = value;
                OnPropertyChanged(nameof(TopGlyph));
            }
        }
        
        #endregion

        public MainViewModel()
        {
            SongClickCommand = new Command(SongClickHandler);
            StopCommand = new Command(StopCommandHandler);
            HideDonationPageCommand = new Command(HideDonationPageHandler);
            ShowMyAppListCommand = new Command(ShowMyAppListHandler);
            HideMyAppsCommand = new Command(HideMyAppsHandler);
            MyAppSelectedCommand = new Command(MyAppSelectedHandler);
            StopRecordCommand = new Command(StopRecordHandler);
            HideADMPPopupCommmand = new Command(HideADMPPopupHandler);
            HideRecordPopupCommand = new Command(HideRecordPopupdHandler);
            SettingsBtnClickedCommand = new Command(TopBtnClickHandler);

            MyApps = new ObservableRangeCollection<MyApp>();
            PlayingSong = new ObservableRangeCollection<ButtonSmileViewModel>();

            _smileButtonSourceService = new SmileButtonSourceService();
            _firebaseService = new FirebaseService();
            _soundService = DependencyService.Get<ISoundService>();

            _firebaseService.Initialize();

            ViewModelItemSource = new SmileItemSourceViewModel(_smileButtonSourceService.GetSmiles());

            Connectivity.ConnectivityChanged += ConnectivityChangedHandler;
            PlayingSong.CollectionChanged += PlayingSongsChanged;

            _soundService.SongReleased += SongReleasedHandler;
            _soundService.RecordReleased += RecordReleasedHandler;
            _soundService.AppClosed += AppClosedHandler;
        }

        #region Methods

        private void PlayingSongsChanged(object sender, NotifyCollectionChangedEventArgs e)
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

                    if (PlayingSong.Count == 0) StopButtonVisible = false;
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
            if (PlayingSong.Count < ConstantsForms.MaxCountOfSoundInOneTime) TooMuchSoundsInOneTime = false;
        }

        private void RequestToDisableSmile(ButtonSmileViewModel viewModel)
        {
            var viewModels = PlayingSong.Where(x =>
                x.CommandParameter == viewModel.CommandParameter);

            if (!viewModels.Any()) viewModel.IsPlaying = false;
        }

        private async Task PrepareToRecording(ButtonSmileViewModel buttonSmileViewModel)
        {
            await StopSounds();

            RecordViewVisible = true;

            _recordProgressTimer = new Timer(500) {AutoReset = true};
            _recordProgressTimer.Elapsed += OnTimedEvent;
            _recordProgressTimer.Start();

            await _soundService.StartRecording(buttonSmileViewModel.CommandParameter);
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (RecordingViewProgress < 100)
            {
                RecordingViewProgress += ConstantsForms.AdditionalValueToRecordProgress;
            }
        }

        public async Task StopRecording(bool closeWithoutPopup = false)
        {
            if (closeWithoutPopup)
            {
                await _soundService.StopRecording();
                _soundService.DeleteSong(_lastClickedViewModel.CommandParameter);
                StopRecordingOnUi();
            }
            else
            {
                StopRecordingPopupVisible = true;
            }
        }

        private void StopRecordingOnUi()
        {
            RecordViewVisible = false;
            RecordingViewProgress = 0;

            _recordProgressTimer?.Stop();
            _recordProgressTimer = null;
        }

        private async Task StopSounds()
        {
            await _soundService.StopPlayingAll();
            PlayingSong.ForEach(x => x.IsPlaying = false);
            PlayingSong.Clear();
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

            PlayingSong.Add(smileViewModel);

            try
            {
                _soundService.CreateSoundPathAndPlay(smileViewModel.CommandParameter);
            }
            catch (Exception e)
            {
                StopSounds();
            }
        }

        private void HideAllRecordButtons()
        {
            var smileViewModels = ViewModelItemSource.ItemSource.Where(x => x.IsRecord);
            foreach (var viewModel in smileViewModels)
            {
                viewModel.IsVisible = false;
            }
        }

        #endregion

        #region Handlers

        private async void SongClickHandler(object obj)
        {
            try
            {
                var soundParameter = (SoundParameter) obj;

                if (!MicrophoneIsDisabledByUser)
                {
                    var permissions = await _soundService.CheckPermissions();
                    if (permissions != PermissionStatus.Granted) ADMPPopupVisible = true;
                }
                
                var smileViewModel =
                    ViewModelItemSource.ItemSource.FirstOrDefault(x => x.CommandParameter == soundParameter);
                if (DeleteRecordsMode)
                { 
                    if (!_soundService.MicrophonePermissionsGranted ||
                        !smileViewModel.IsRecord || 
                        smileViewModel.IsPlusButton) return;
                    smileViewModel.SwitchDeleteUIState();
                }
                else
                {
                    _lastClickedViewModel = smileViewModel;
                    if (smileViewModel != null && smileViewModel.IsPlusButton)
                    {
                        await PrepareToRecording(smileViewModel);
                    }
                    else
                    {
                        if (PlayingSong.Count < ConstantsForms.MaxCountOfSoundInOneTime)
                        {
                            await PrepareToPlaySound(smileViewModel);
                        }
                        else
                        {
                            TooMuchSoundsInOneTime = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void TopBtnClickHandler(object obj)
        {
            if ((string)obj == ConstantsForms.Positive)
            {
                var viewModels = ViewModelItemSource.ItemSource.Where(item => 
                    item.DeleteModeState == DeleteModeState.ToDelete).ToList();

                if (viewModels.Count == 0)
                {
                    DeleteRecordsMode = !DeleteRecordsMode;
                    return;
                }
                
                foreach (var viewModel in viewModels)
                {
                    var songPath = ResourceHelper.GetSongPath(viewModel.CommandParameter);
                    var fileExist = System.IO.File.Exists(songPath);
                    if (!fileExist) continue;
                    try
                    {
                        System.IO.File.Delete(songPath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            DeleteRecordsMode = !DeleteRecordsMode;
        }

        private void MyAppSelectedHandler(object obj)
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

        private void SongReleasedHandler(SoundParameter parameter)
        {
            var model = PlayingSong.FirstOrDefault(x => x.CommandParameter == parameter);
            if (model != null) PlayingSong.Remove(model);
        }

        private void RecordReleasedHandler()
        {
            if (_lastClickedViewModel.IsPlusButton)
            {
                var previousSongIsPlusButton = false;
                foreach (var song in ViewModelItemSource.ItemSource)
                {
                    if (previousSongIsPlusButton)
                    {
                        song.IsVisible = true;
                        song.IsPlusButton = true;
                        song.IsPlaying = false;
                        break;
                    }

                    if (song.IsPlusButton)
                    {
                        // why microphone is hidden
                        previousSongIsPlusButton = true;
                        song.IsVisible = true;
                        song.IsPlusButton = false;
                        song.IsPlaying = false;
                        song.Image = SoundParameter.smilingface + ConstantsForms.ImageExtension;
                        song.SongPath = ResourceHelper.GetSongPath(_lastClickedViewModel.CommandParameter);
                    }
                }
            }

            StopRecordingOnUi();
            StopRecordingOnUi();
            
            TopButtonVisible = true;
        }

        private async void StopCommandHandler(object obj)
        {
            StopSounds();
        }

        private void StopRecordHandler()
        {
            StopRecording();
        }

        private void ShowMyAppListHandler()
        {
            MyAppsPageVisible = true;
            DonationPageVisible = false;
        }

        private void HideMyAppsHandler()
        {
            MyAppsPageVisible = false;
        }

        private void HideDonationPageHandler()
        {
            DonationPageVisible = false;
        }

        private void HideRecordPopupdHandler(object obj)
        {
            var param = obj as string;
            if (param == ConstantsForms.Positive)
            {
                _soundService.StopRecording();
            }

            StopRecordingOnUi();
            StopRecordingPopupVisible = false;
        }

        private async void HideADMPPopupHandler(object obj)
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