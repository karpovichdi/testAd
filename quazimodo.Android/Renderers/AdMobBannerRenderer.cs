using Android.Content;
using Android.Gms.Ads;
using Android.Widget;
using quazimodo.Controlls;
using quazimodo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdMobBannerView), typeof(AdMobBannerRenderer))]
namespace quazimodo.Droid.Renderers
{
    public class AdMobBannerRenderer : ViewRenderer<AdMobBannerView, AdView>
    {
        public AdMobBannerRenderer(Context context) : base(context) { }
 
        private AdView CreateAdView()
        {
            var adView = new AdView(MainActivity.Instance)
            {
                AdSize = AdSize.Banner,
                AdUnitId = Element.AdId,
                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent),
            };

            adView.LoadAd(new AdRequest.Builder().Build());
            return adView;
        }
 
        protected override void OnElementChanged(ElementChangedEventArgs<AdMobBannerView> e)
        {
            base.OnElementChanged(e);
 
            if (e.NewElement != null && Control == null)
            {
                SetNativeControl(CreateAdView());
            }
        }
    }
}