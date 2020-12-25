using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quazimodo.Views.Controlls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageFontView : ContentView
    {
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter), typeof(string), typeof(ImageFontView), null);
        
        public static readonly BindableProperty GlyphProperty = BindableProperty.Create(
            nameof(Glyph), typeof(string), typeof(ImageFontView), null);
        
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color), typeof(Color), typeof(ImageFontView), null);

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            nameof(Size), typeof(double), typeof(ImageFontView), null);
        
        public string Glyph
        {
            get => (string)GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }
        
        public string CommandParameter
        {
            get => (string)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
        
        public ImageFontView()
        {
            InitializeComponent();
        }
    }
}