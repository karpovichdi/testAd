using System;
using quazimodo.Constants;
using quazimodo.Interfaces;
using quazimodo.ViewModels;
using Xamarin.Forms;

namespace quazimodo.Views
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel => BindingContext as MainViewModel;
        
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            _viewModel.StopButtonVisible = false;
            base.OnDisappearing();
        }
    }
}