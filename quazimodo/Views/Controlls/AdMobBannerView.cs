using Xamarin.Forms;

namespace quazimodo.Views.Controlls
{
    public class AdMobBannerView : View
    {
        public static readonly BindableProperty AdIdProperty = BindableProperty.Create(nameof(AdId),  
            typeof(string), typeof(AdMobBannerView));  
  
        public string AdId
        {  
            get => (string)GetValue(AdIdProperty);
            set => SetValue(AdIdProperty, value);
        }
    }
}