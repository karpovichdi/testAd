using System;
using System.Threading.Tasks;
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

        private void NextPageClicked(object sender, EventArgs e)
        {
            NavigateToNewPage();
        }

        private async Task NavigateToNewPage()
        {
            try
            {
                await Navigation.PushAsync(new VideoAddPage());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
