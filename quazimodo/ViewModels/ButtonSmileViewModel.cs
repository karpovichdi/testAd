using quazimodo.Models.Enums;
using Xamarin.Forms;

namespace quazimodo.ViewModels
{
    public class ButtonSmileViewModel : BindableObject
    {
        private ImageSource _image;
        private string _songPath;
        private bool _isPlaying;
        private bool _isPlusButton;
        
        public SoundParameter CommandParameter { get; set; }
        public SmileType SmileType { get; set; }
        public bool IsRecord { get; set; }
        public bool IsVisibleRecord { get; set; }

        public ImageSource Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        
        public string SongPath
        {
            get => _songPath;
            set
            {
                _songPath = value;
                OnPropertyChanged(nameof(SongPath));
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