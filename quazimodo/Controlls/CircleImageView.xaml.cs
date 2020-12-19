using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quazimodo.Controlls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircleImageView : Grid
    {
        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(
            nameof(Progress), typeof(double), typeof(CircularProgressbarView),propertyChanged:OnProgressChanged);
        
        public static readonly BindableProperty CircleColorProperty = BindableProperty.Create(
            nameof(CircleColor), typeof(Color), typeof(CircularProgressbarView),propertyChanged:OnCircleColorChanged);

        public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(
            nameof(ProgressColor), typeof(Color), typeof(CircularProgressbarView),propertyChanged:OnProgressColorChanged);
        
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(CircularProgressbarView), null);
        
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        public double Progress
        {
            get => (double) GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
        
        public string ProgressColor
        {
            get => (string) GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
        
        public string CircleColor
        {
            get => (string) GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
        
        public CircleImageView()
        {
            InitializeComponent();
        }
        
        private static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CircleImageView circleImageView) circleImageView.circularProgressbarView.Progress = (double)newValue;
        }
        
        private static void OnCircleColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CircleImageView circleImageView) circleImageView.circularProgressbarView.CircleColor = (Color)newValue;
        }
        
        private static void OnProgressColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CircleImageView circleImageView) circleImageView.circularProgressbarView.ProgressColor = (Color)newValue;
        }
    }
}