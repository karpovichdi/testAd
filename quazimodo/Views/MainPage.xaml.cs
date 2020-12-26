using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using quazimodo.Utilities.Constants;
using quazimodo.ViewModels;
using quazimodo.Views.Controlls;
using quazimodo.Views.Popups;
using Xamarin.Forms;

namespace quazimodo.Views
{
    public partial class MainPage : ContentPage
    {
        private const int DelayTime = 100;

        private bool _myAppsViewAdded;
        private bool _donationPageAdded;
        private bool _recordViewAdded;
        private bool _stopRecordViewAdded;
        private bool _admpViewAdded;

        public static Stopwatch PageTimer;
        public static Stopwatch AppTimer;

        private MainViewModel ViewModel => BindingContext as MainViewModel;
        
        public MainPage()
        {
            PageTimer = Stopwatch.StartNew();
            
            InitializeComponent();
            
            // Page timer: 2.312
            // Page timer: 2.261
            // Page timer: 1.774
            // Page timer: 1,86  App timer: 3,52
            // Page timer: 1,714 App timer: 3,372
            // Page timer: 1,587 App timer: 3,237
            // Page timer: 2,038 App timer: 3,707
            // Page timer: 1,708 App timer: 3,418
            // Page timer: 1,803 App timer: 3,503
            // Page timer: 1,759 App timer: 3,581
            // Page timer: 1,786 App timer: 3,351
            // Page timer: 1,673 App timer: 3,204
            // Page timer: 1,801 App timer: 3,436
            // Page timer: 1,632 App timer: 3,23
            // Page timer: 1,728 App timer: 3,383
            // Page timer: 1,793 App timer: 3,433
            // Page timer: 1,78 App timer: 3,439
            StopTimers();
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged; 
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.MyAppsPageVisible):
                    AddMyAppPageToHierarchy();
                    break;
                case nameof(ViewModel.DonationPageVisible):
                    AddDonationPageToHierarchy();
                    break;
                case nameof(ViewModel.RecordViewVisible):
                    AddRecordViewToHierarchy();
                    break;
                case nameof(ViewModel.StopRecordingPopupVisible):
                    AddStopRecordViewToHierarchy();
                    break;
                case nameof(ViewModel.ADMPPopupVisible):
                    AddADMPViewToHierarchy();
                    break;
            }
        }
        
        #region Methods
        
        private void AddADMPViewToHierarchy()
        {
            if (_admpViewAdded) return;

            var admpPopup = new MicPermissionsPopup
            {
                BindingContext = ViewModel,
                Command = ViewModel.HideADMPPopupCommmand,
                Opacity = 0.8,
                BackgroundColor = ConstantsForms.MarkupResources.DarkPurple,
                HeightRequest = ConstantsForms.MarkupResources.ProgressViewSize,
                WidthRequest = ConstantsForms.MarkupResources.ProgressViewSize
            };

            rootView.Children.Add(admpPopup, 0,1,0,3);

            admpPopup.SetBinding(IsVisibleProperty, nameof(ViewModel.ADMPPopupVisible));
            
            _admpViewAdded = true;
        }
        
        private void AddStopRecordViewToHierarchy()
        {
            if (_stopRecordViewAdded) return;

            var stopRecordPopup = new StopRecordPopup
            {
                BindingContext = ViewModel,
                Command = ViewModel.HideRecordPopupCommand,
                Opacity = 0.8,
                BackgroundColor = ConstantsForms.MarkupResources.DarkPurple,
                HeightRequest = ConstantsForms.MarkupResources.ProgressViewSize,
                WidthRequest = ConstantsForms.MarkupResources.ProgressViewSize
            };

            rootView.Children.Add(stopRecordPopup, 0,1,0,3);

            stopRecordPopup.SetBinding(IsVisibleProperty, nameof(ViewModel.StopRecordingPopupVisible));
            
            _stopRecordViewAdded = true;
        }
        
        private void AddRecordViewToHierarchy()
        {
            if (_recordViewAdded) return;

            var circleImageView = new CircleImageView
            {
                BindingContext = ViewModel,
                Command = ViewModel.StopRecordCommand,
                Opacity = 0.8,
                BackgroundColor = ConstantsForms.MarkupResources.DarkPurple,
                HeightRequest = ConstantsForms.MarkupResources.ProgressViewSize,
                WidthRequest = ConstantsForms.MarkupResources.ProgressViewSize
            };

            rootView.Children.Add(circleImageView, 0,1,0,3);

            circleImageView.SetBinding(IsVisibleProperty, nameof(ViewModel.RecordViewVisible));
            circleImageView.SetBinding(CircleImageView.ProgressProperty, nameof(ViewModel.RecordingViewProgress));
            
            _recordViewAdded = true;
        }
        
        private void AddDonationPageToHierarchy()
        {
            if (_donationPageAdded) return;
            
            var donationView = new DonationView();
            rootView.Children.Add(donationView, 0,1,0,2);

            donationView.BindingContext = ViewModel;
            donationView.SetBinding(IsVisibleProperty, nameof(ViewModel.DonationPageVisible));
            _donationPageAdded = true;
        }
        
        private void AddMyAppPageToHierarchy()
        {
            if (_myAppsViewAdded) return;
            
            var myAppsView = new MyAppsView();
            rootView.Children.Add(myAppsView, 0,1,0,2);

            myAppsView.BindingContext = ViewModel;
            myAppsView.SetBinding(IsVisibleProperty, nameof(ViewModel.MyAppsPageVisible));
            _myAppsViewAdded = true;
        }
        
        public static void StartTimer()
        {
            AppTimer = Stopwatch.StartNew();
        }
        
        public static void StopTimers()
        {
            PageTimer.Stop();
            var elapsedMs = (double)PageTimer.ElapsedMilliseconds / 1000;
            Debug.WriteLine("Page timer: " + elapsedMs);
            
            AppTimer.Stop();
            var elapsedMs2 = (double)AppTimer.ElapsedMilliseconds / 1000;
            Debug.WriteLine("App timer: " + elapsedMs2);
        }
        
        #endregion

        #region Overrides
        
        protected override void OnDisappearing()
        {
            ViewModel.StopButtonVisible = false;
            base.OnDisappearing();
        }

        protected override async void OnAppearing()
        {
            if (!ViewModel.AppsIsLoaded)
            {
                ViewModel.FillMyAppList();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (ViewModel.DonationPageVisible || ViewModel.MyAppsPageVisible)
            {
                ViewModel.DonationPageVisible = false;
                ViewModel.MyAppsPageVisible = false;
                ViewModel.StopRecording(true);
            }
            else
            {
                return base.OnBackButtonPressed();
            }

            return true;
        }
        
        #endregion
    }
}