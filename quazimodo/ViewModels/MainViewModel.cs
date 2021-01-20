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
using Xamarin.Forms.Xaml;

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
        private bool _stopRecordingPopupVisible;
        private bool _deleteRecordsMode;
        private bool _admpPopupVisible;
        private bool _topButtonVisible;
        private bool _microphoneIsDisabledByUser;
        private bool _recordsVisible;
        private double _recordingViewProgress;
        private string _topGlyph = FontIcons.cog;
        private Timer _recordProgressTimer;
        private SoundParameter _lastRecordedSoundParameter;

        #endregion

        #region Properties

        public bool IsRecording { get; set; }
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
        public Command RecordButtonCommand { get; set; }
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
                if (value)
                {
                    HideAllRecordButtons();
                }
                else
                {
                    ShowAllRecordButtons();
                }
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
            HideRecordPopupCommand = new Command(HideRecordPopupHandler);
            SettingsBtnClickedCommand = new Command(TopBtnClickHandler);
            RecordButtonCommand = new Command(RecordButtonHandler);

            MyApps = new ObservableRangeCollection<MyApp>();
            PlayingSong = new ObservableRangeCollection<ButtonSmileViewModel>();

            _smileButtonSourceService = new SmileButtonSourceService();
            _firebaseService = new FirebaseService();
            _soundService = DependencyService.Get<ISoundService>();

            _firebaseService.Initialize();

            ViewModelItemSource = new SmileItemSourceViewModel(_smileButtonSourceService.GetSmiles(), _soundService);

            Connectivity.ConnectivityChanged += ConnectivityChangedHandler;
            PlayingSong.CollectionChanged += PlayingSongsChanged;

            _soundService.SongReleased += SongReleasedHandler;
            _soundService.RecordReleased += RecordReleasedHandler;
            _soundService.AppClosed += AppClosedHandler;
        }

        private async void RecordButtonHandler()
        {
            IsRecording = true;
            
            await PrepareToRecording();
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

        private async Task PrepareToRecording()
        {
            await StopSounds();

            RecordViewVisible = true;

            _recordProgressTimer = new Timer(500) {AutoReset = true};
            _recordProgressTimer.Elapsed += OnTimedEvent;
            _recordProgressTimer.Start();

            _lastRecordedSoundParameter = Helpers.GetNewSoundParameter();
            await _soundService.StartRecording(_lastRecordedSoundParameter);
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
                await _soundService.StopRecording(true);
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
            // var plusButton1 = ViewModelItemSource.PositiveItemSource.FirstOrDefault(x => x.IsPlusButton);
            // var plusButton2 = ViewModelItemSource.NeutralItemSource.FirstOrDefault(x => x.IsPlusButton);
            // var plusButton3 = ViewModelItemSource.NegativeItemSource.FirstOrDefault(x => x.IsPlusButton);

            // ViewModelItemSource.ItemSource.Remove(plusButton1);
            // ViewModelItemSource.ItemSource.Remove(plusButton2);
            // ViewModelItemSource.ItemSource.Remove(plusButton3);
        }

        private void ShowAllRecordButtons()
        {
            var recordsCount = ViewModelItemSource.RecordsItemSource.Count(x => x.SmileType == SmileType.Record);
            if (recordsCount < ConstantsForms.MaxCountOfRecords)
            {
                var buttonSmileViewModel = new ButtonSmileViewModel
                {
                    SmileType = SmileType.Record,
                    CommandParameter = Helpers.GetNewSoundParameter()
                };
                ViewModelItemSource.ItemSource.Add(buttonSmileViewModel);
            }
        }

        #endregion

        #region Handlers

        private async void SongClickHandler(object obj)
        {
            try
            {
                var soundParameter = (SoundParameter) obj;
                Debug.WriteLine("SoundParameter: " + soundParameter);
                var smileViewModel =
                    ViewModelItemSource.ItemSource.FirstOrDefault(x => x.CommandParameter == soundParameter);

                if (smileViewModel == null) return;
                if (!MicrophoneIsDisabledByUser)
                {
                    if (await _soundService.RequestPermissionsIfNeeded() != PermissionStatus.Granted)
                    {
                        ADMPPopupVisible = true;
                        return;
                    }
                }

                if (MicrophoneIsDisabledByUser) return;
                if (DeleteRecordsMode)
                {
                    if (!_soundService.MicrophonePermissionsGranted ||
                        smileViewModel.SmileType != SmileType.Record) return;
                    smileViewModel.SwitchDeleteUIState();
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DeleteAllRecords()
        {
            var records = new SoundParameter[]
            {
                SoundParameter.record1,
                SoundParameter.record2,
                SoundParameter.record3,
                SoundParameter.record4,
                SoundParameter.record5,
                SoundParameter.record6,
                SoundParameter.record7,
                SoundParameter.record8,
                SoundParameter.record9,
                SoundParameter.record10,
                SoundParameter.record11,
                SoundParameter.record12,
                SoundParameter.record13,
                SoundParameter.record14,
                SoundParameter.record15,
                SoundParameter.record16,
                SoundParameter.record17,
                SoundParameter.record18,
                SoundParameter.record19,
                SoundParameter.record20,
                SoundParameter.record21,
                SoundParameter.record22,
                SoundParameter.record23,
                SoundParameter.record24,
                SoundParameter.record25,
                SoundParameter.record26,
                SoundParameter.record27,
                SoundParameter.record28,
                SoundParameter.record29,
                SoundParameter.record30,
            };

            foreach (var record in records)
            {
                var songPath = Helpers.GetSongPath(record);
                var fileExist = System.IO.File.Exists(songPath);
                if (fileExist)
                {
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
        }

        private void TopBtnClickHandler(object obj)
        {
            if ((string) obj == ConstantsForms.Positive)
            {
                // SmileType deleteSmileTypes = SmileType.NotSet;

                var viewModels = ViewModelItemSource.ItemSource.Where(item =>
                    item.DeleteModeState == DeleteModeState.ToDelete).ToList();

                if (viewModels.Count == 0)
                {
                    DeleteRecordsMode = !DeleteRecordsMode;
                    return;
                }

                foreach (var viewModel in viewModels)
                {
                    DeleteSong(viewModel.CommandParameter);


                    // deleteSmileTypes |= viewModel.SmileType;
                }

                // var smileTypes = Enum.GetValues(typeof(SmileType)).Cast<SmileType>();
                // foreach (var smileType in smileTypes)
                // {
                //     if (smileType == SmileType.NotSet) continue;
                //     if (deleteSmileTypes.HasFlag(smileType))
                //     {
                //         var itemSourceByType = ViewModelItemSource.GetSmileItemSourceByType(_lastClickedViewModel.SmileType);
                //         var visibleRecords = itemSourceByType.Where(x => x.IsRecord && x.IsVisible);
                //         
                //     }
                // }
            }

            DeleteRecordsMode = !DeleteRecordsMode;
        }

        private void DeleteSong(SoundParameter parameter)
        {
            var songPath = Helpers.GetSongPath(parameter);
            var fileExist = System.IO.File.Exists(songPath);
            if (!fileExist) return;
            try
            {
                var viewModel = ViewModelItemSource.ItemSource.GetViewModelByParameter(parameter);

                viewModel.SongPath = "";
                viewModel.Image = "";
                ViewModelItemSource.ItemSource.Remove(viewModel);

                System.IO.File.Delete(songPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
            var model = PlayingSong.GetViewModelByParameter(parameter);
            if (model != null) PlayingSong.Remove(model);
        }

        private void RecordReleasedHandler()
        {
            if (IsRecording)
            {
                var viewModel = new ButtonSmileViewModel
                {
                    SongPath = Helpers.GetSongPath(_lastRecordedSoundParameter),
                    Image = ImageSource.FromFile($"{_lastRecordedSoundParameter}.png"),
                    CommandParameter = _lastRecordedSoundParameter,
                    SmileType = SmileType.Record
                };
                
                ViewModelItemSource.ItemSource.Add(viewModel);
            }

            IsRecording = false;
            
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
            DeleteAllRecords();

            // MyAppsPageVisible = true;
            // DonationPageVisible = false;
        }

        private void HideMyAppsHandler()
        {
            MyAppsPageVisible = false;
        }

        private void HideDonationPageHandler()
        {
            DonationPageVisible = false;
        }

        private async void HideRecordPopupHandler(object obj)
        {
            var param = obj as string;
            if (param == ConstantsForms.Positive)
            {
                await _soundService.StopRecording(false);
            }
            else
            {
                await _soundService.StopRecording(true);
            }

            StopRecordingOnUi();
            StopRecordingPopupVisible = false;
        }

        private void HideADMPPopupHandler(object obj)
        {
            MicrophoneIsDisabledByUser = true;
            ADMPPopupVisible = false;
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