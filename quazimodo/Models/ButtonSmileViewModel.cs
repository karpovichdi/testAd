using quazimodo.Enums;
using Xamarin.Forms;

namespace quazimodo.Models
{
    public class ButtonSmileViewModel : BindableObject
    {
        private bool _isPlaying;
        
        public SoundParameter CommandParameter { get; set; }
        public string Image { get; set; }
        public SmileType SmileType { get; set; }

        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                OnPropertyChanged(nameof(IsPlaying));
            }
        }
    }
}