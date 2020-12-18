using quazimodo.Enums;
using Xamarin.Forms;

namespace quazimodo.Models
{
    public class ButtonSmileViewModel : BindableObject
    {
        private bool _isPlaying;
        private bool _isPlusButton;
        private ImageSource _image;

        public SoundParameter CommandParameter { get; set; }
        public SmileType SmileType { get; set; }
        public bool IsRecord { get; set; }
        public bool IsVisibleRecord { get; set; }

        public ImageSource Image // TODO try to delete binding for this property
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        
        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                OnPropertyChanged(nameof(IsPlaying));
            }
        }
        
        public bool IsPlusButton
        {
            get => _isPlusButton;
            set
            {
                _isPlusButton = value;
                OnPropertyChanged(nameof(IsPlusButton));
            }
        }
    }
}