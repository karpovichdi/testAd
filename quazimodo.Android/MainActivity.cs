using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using quazimodo.Droid.Listeners;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Platform = Xamarin.Essentials.Platform;

namespace quazimodo.Droid
{
    [Activity(Label = "Tagura",
              Theme = "@style/LaunchTheme",
              MainLauncher = true, 
              ConfigurationChanges = ConfigChanges.ScreenSize | 
                                     ConfigChanges.Orientation | 
                                     ConfigChanges.UiMode | 
                                     ConfigChanges.ScreenLayout | 
                                     ConfigChanges.SmallestScreenSize)]
    public class MainActivity : FormsAppCompatActivity
    {
        public static MainActivity Instance { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Instance = this;
            
            Platform.Init(this, savedInstanceState);
            MobileAds.Initialize(ApplicationContext, new AdMobInitializationListener());
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}