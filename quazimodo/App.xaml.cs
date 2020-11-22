using System;
using quazimodo.Interfaces;
using quazimodo.Views;
using Xamarin.Forms;

namespace quazimodo
{
    public partial class App : Application
    {
        public static int CountOfPlayedSound;
        
        private readonly IStorageService _storageService;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage()); 
            
            _storageService = DependencyService.Get<IStorageService>();
        }

        protected override void OnStart()
        {
            SetCount();
        }

        protected override void OnResume()
        {
            SetCount();
        }
        
        protected override void OnSleep()
        {
            try
            {
                DependencyService.Get<IAudioService>().StopPlaying();
                _storageService.SaveCount(CountOfPlayedSound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void SetCount()
        {
            try
            {
                CountOfPlayedSound = _storageService.GetCount();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
