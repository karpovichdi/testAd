using System.Windows.Input;
using quazimodo.Views.Controlls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quazimodo.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApproveDeclaimMicPermissionsPopup : ContentView
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(ConfirmPopup), null);
        
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        public ApproveDeclaimMicPermissionsPopup()
        {
            InitializeComponent();
        }
    }
}