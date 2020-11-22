﻿using quazimodo.ViewModels;
using Xamarin.Forms;

namespace quazimodo.Views
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;
        
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            ViewModel.StopButtonVisible = false;
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            ViewModel.FillMyAppList();
        }
    }
}