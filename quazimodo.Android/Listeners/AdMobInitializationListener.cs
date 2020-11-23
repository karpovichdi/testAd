using Android.Gms.Ads.Initialization;

namespace quazimodo.Droid.Listeners
{
    public class AdMobInitializationListener : Java.Lang.Object, IOnInitializationCompleteListener
    {
        public void OnInitializationComplete(IInitializationStatus p0)
        {
            App.AdInitialized = true;
        }
    }
}