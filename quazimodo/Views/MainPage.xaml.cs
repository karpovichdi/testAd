using System.ComponentModel;
using System.Threading.Tasks;
using quazimodo.ViewModels;
using quazimodo.Views.Controlls;
using Xamarin.Forms;

namespace quazimodo.Views
{
    public partial class MainPage : ContentPage
    {
        private bool _myAppsViewAdded;
        private MainViewModel ViewModel => BindingContext as MainViewModel;
        
        public MainPage()
        {
            InitializeComponent();
            
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged; 
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.MyAppsPageVisible):
                    AddMyAppPageToHierarchy();
                    break;
            }
        }

        private async void AddMyAppPageToHierarchy()
        {
            if (_myAppsViewAdded) return;
            ViewModel.IsBusy = true;

            await Task.Delay(500);
            
            var myAppsView = new MyAppsView();
            rootView.Children.Add(myAppsView, 0,1,0,2);

            myAppsView.BindingContext = ViewModel;
            myAppsView.SetBinding(IsVisibleProperty, nameof(ViewModel.MyAppsPageVisible));
            _myAppsViewAdded = true;
            ViewModel.IsBusy = false;
        }

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
    }
}