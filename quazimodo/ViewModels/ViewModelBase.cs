using Xamarin.Essentials;
using Xamarin.Forms;

namespace quazimodo.ViewModels
{
    public class ViewModelBase : BindableObject
    {
        private bool _isBusy;

        public ConnectionProfile ConnectionProfile { get; private set; }        

        #region Properties

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        #endregion
    }
}