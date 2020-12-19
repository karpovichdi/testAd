using quazimodo.Utilities.Constants;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quazimodo.Views.Controlls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircularProgressbarView : ContentView
    {
        private ProgressDrawer _progressDrawer;

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(
            nameof(Progress), typeof(double), typeof(CircularProgressbarView),propertyChanged:OnProgressChanged);

        public static readonly BindableProperty CircleColorProperty = BindableProperty.Create(
            nameof(CircleColor), typeof(Color), typeof(CircularProgressbarView),propertyChanged:OnCircleColorChanged);

        public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(
            nameof(ProgressColor), typeof(Color), typeof(CircularProgressbarView),propertyChanged:OnProgressColorChanged);
        
        public double Progress
        {
            get => (double) GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
        
        public Color ProgressColor
        {
            get => (Color) GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
        
        public Color CircleColor
        {
            get => (Color) GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
        
        public CircularProgressbarView()
        {
            InitializeComponent();

            DrawIndicatorView(ConstantsForms.MarkupResources.Gray, ConstantsForms.MarkupResources.Red, this);
        }

        private static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var context = bindable as CircularProgressbarView;
            context?.skCanvasView.InvalidateSurface();
        }
        
        private static void OnCircleColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CircularProgressbarView circularProgressbar)
            {
                DrawIndicatorView((Color)newValue, circularProgressbar.ProgressColor, circularProgressbar);
            }
        }

        private static void OnProgressColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CircularProgressbarView circularProgressbar)
            {
                DrawIndicatorView(circleColor: circularProgressbar.CircleColor, progressColor:(Color)newValue, circularProgressbar);
            }
        }

        private static void DrawIndicatorView(Color circleColor, Color progressColor, CircularProgressbarView circularProgressbar)
        {
            var circle = new ProgressDrawer.Circle(400,(info)=> new SKPoint((float)info.Width / 2,(float)info.Height / 2));
            circularProgressbar._progressDrawer = new ProgressDrawer(circularProgressbar.skCanvasView, circle, () => (float)circularProgressbar.Progress, 5,SKColor.Parse(circleColor.ToHex()), SKColor.Parse(progressColor.ToHex()));
        }
    }
}