using System;

using Android.App;
using Android.Content.PM;
using Android.Gms.Ads.Initialization;
using Android.Runtime;
using Android.OS;

using Java.Interop;


namespace quazimodo.Droid
{
    [Activity(Label = "quazimodo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private string USER_ID;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Instance = this;
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            try
            {
                Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, new MyClass());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // ConfigFile.GetConfigFile().SetPluginData("Xamarin", IronSourceUtils.SDKVersion, "10");
            // IronSource.Init(this, Constants.IronSourceAppId, IronSource.AD_UNIT.RewardedVideo);
            // IronSource.ShouldTrackNetworkState(this, true);
            // IntegrationHelper.ValidateIntegration(this);
            
            // IronSource.SetRewardedVideoListener(new RewarderVideoListener());
            
            // USER_ID = IronSource.GetAdvertiserId(this);
            //
            // IronSource.SetDynamicUserId(USER_ID);
            
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public class MyClass : Java.Lang.Object, IOnInitializationCompleteListener 
        {
            public void OnInitializationComplete(IInitializationStatus p0)
            {
                
            }
        }
        
        protected override void OnPause()
        {
            base.OnPause();
            // IronSource.OnPause(this);
        }
        protected override void OnResume()
        {
            base.OnResume();
            // IronSource.OnResume(this);
        }
        
        public static MainActivity Instance { get; set; }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    // public class RewarderVideoListener : IRewardedVideoListener, Java.Lang.
    // {
    //     public void OnRewardedVideoAdClicked(Placement placement)
    //     {
    //     }
    //
    //     public void OnRewardedVideoAdClosed()
    //     {
    //     }
    //
    //     public void OnRewardedVideoAdEnded()
    //     {
    //     }
    //
    //     public void OnRewardedVideoAdOpened()
    //     {
    //     }
    //
    //     public void OnRewardedVideoAdRewarded(Placement p0)
    //     {
    //     }
    //
    //     public void OnRewardedVideoAdShowFailed(IronSourceError error)
    //     {
    //     }
    //
    //     public void OnRewardedVideoAdStarted()
    //     {
    //     }
    //
    //     public void OnRewardedVideoAvailabilityChanged(bool available)
    //     {
    //     }
    //
    //     public void Dispose()
    //     {
    //         
    //     }
    //
    //     public IntPtr Handle { get; }
    //     public void SetJniIdentityHashCode(int value)
    //     {
    //     }
    //
    //     public void SetPeerReference(JniObjectReference reference)
    //     {
    //     }
    //
    //     public void SetJniManagedPeerState(JniManagedPeerStates value)
    //     {
    //     }
    //
    //     public void UnregisterFromRuntime()
    //     {
    //     }
    //
    //     public void DisposeUnlessReferenced()
    //     {
    //     }
    //
    //     public void Disposed()
    //     {
    //     }
    //
    //     public void Finalized()
    //     {
    //     }
    //
    //     public int JniIdentityHashCode { get; }
    //     public JniObjectReference PeerReference { get; }
    //     public JniPeerMembers JniPeerMembers { get; }
    //     public JniManagedPeerStates JniManagedPeerState { get; }
    // }
}