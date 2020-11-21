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
            CountOfPlayedSound = _storageService.GetCount();
        }

        protected override void OnSleep()
        {
            DependencyService.Get<IAudioService>().StopPlaying();
            _storageService.SaveCount(CountOfPlayedSound);
        }

        protected override void OnResume()
        {
            CountOfPlayedSound = _storageService.GetCount();
        }
    }
}
