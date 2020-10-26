using System;
using Android.Content;
using Android.Gms.Ads;
using Android.Widget;
using quazimodo;
using quazimodo.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace quazimodo.Droid
{
    public class AdMobViewRenderer : ViewRenderer<AdMobView, AdView>
    {
        public AdMobViewRenderer(Context context) : base(context) { }
 
        private AdView CreateAdView()
        {
            var adView = new AdView(MainActivity.Instance)
            {
                AdSize = AdSize.Banner,
                AdUnitId = Element.AdId,
                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent),
            };

            try
            {
                var adRequest = new AdRequest.Builder();
                adView.LoadAd(adRequest.Build());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
 
            return adView;
        }
 
        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        {
            base.OnElementChanged(e);
 
            if (e.NewElement != null && Control == null)
            {
                SetNativeControl(CreateAdView());
            }
        }
    }
}