using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quazimodo.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MicPermissionsPopup : ContentView
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(MicPermissionsPopup), null);
        
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        public MicPermissionsPopup()
        {
            InitializeComponent();
        }
    }
}