using System;
using quazimodo.Interfaces;
using quazimodo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quazimodo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            DependencyService.Get<IAudioService>().StopPlaying();
        }

        protected override void OnResume()
        {
        }
    }
}
