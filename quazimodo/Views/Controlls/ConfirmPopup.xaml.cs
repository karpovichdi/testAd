using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quazimodo.Views.Controlls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmPopup : ContentView
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(ConfirmPopup), null);
        
        public static readonly BindableProperty NegativeBtnTextProperty = BindableProperty.Create(
            nameof(NegativeBtnText), typeof(string), typeof(ConfirmPopup), null);
        
        public static readonly BindableProperty PositiveBtnTextProperty = BindableProperty.Create(
            nameof(PositiveBtnText), typeof(string), typeof(ConfirmPopup), null);
        
        public static readonly BindableProperty MessageTextProperty = BindableProperty.Create(
            nameof(MessageText), typeof(string), typeof(ConfirmPopup), null);
        
        public string NegativeBtnText
        {
            get => (string)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        public string PositiveBtnText
        {
            get => (string)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        public string MessageText
        {
            get => (string)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        public ConfirmPopup()
        {
            InitializeComponent();
        }
    }
}