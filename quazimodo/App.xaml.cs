using System;
using System.Globalization;
using quazimodo.Interfaces;
using quazimodo.Views;
using Xamarin.Forms;

namespace quazimodo
{
    public partial class App : Application
    {
        public static bool AdInitialized;
        public static int CountOfPlayedSound;
        public static string TwoLetterIsoLanguageName;

        private readonly IStorageService _storageService;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage()); 
            
            _storageService = DependencyService.Get<IStorageService>();
        }
        
        protected override void OnStart()
        {
            Setup();
        }

        protected override void OnResume()
        {
            Setup();
        }
        
        protected override void OnSleep()
        {
            try
            {
                DependencyService.Get<ISoundService>().AppClosed.Invoke();
                _storageService.SaveCount(CountOfPlayedSound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void Setup()
        {
            try
            {
                CountOfPlayedSound = _storageService.GetCount();

                TwoLetterIsoLanguageName = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}