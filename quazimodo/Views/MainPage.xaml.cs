using System;
using quazimodo.Interfaces;
using Xamarin.Forms;

namespace quazimodo.Views
{
    public partial class MainPage : ContentPage
    {
        public static bool Playing;
        
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnStopClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAudioService>().StopPlaying();
        }
    }
}
