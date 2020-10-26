using System;
using Xamarin.Forms;

namespace quazimodo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnStartClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("sound.mp3");
        }

        private void BtnStopClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAudio>().StopPlaying();
        }
    }
}
