using Xamarin.Forms;

namespace quazimodo
{
    public class AdMobView : View
    {
        public static readonly BindableProperty AdIdProperty = BindableProperty.Create(nameof(AdId),  
            typeof(string), typeof(AdMobView));  
  
        public string AdId
        {  
            get => (string)GetValue(AdIdProperty);
            set => SetValue(AdIdProperty, value);
        }
    }
}