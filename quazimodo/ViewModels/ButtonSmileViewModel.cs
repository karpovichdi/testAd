using System;
using quazimodo.Models.Enums;
using quazimodo.Utilities.Constants;
using Xamarin.Forms;

namespace quazimodo.ViewModels
{
    public class ButtonSmileViewModel : BindableObject
    {
        private string _songPath;
        private bool _isPlaying;
        private bool _isPlusButton;
        private bool _isVisible;
        private Style _style;
        private ImageSource _image;

        public SoundParameter CommandParameter { get; set; }
        public SmileType SmileType { get; set; }
        public bool IsRecord { get; set; }

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
                if (value) Image = ConstantsForms.DefaultImageSource;
                _isPlusButton = value;
                OnPropertyChanged(nameof(IsPlusButton));
            }
        }
        
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
        
        public Style Style
        {
            get
            {
                switch (SmileType)
                {
                    case SmileType.Positive:
                        return ConstantsForms.MarkupResources.PositiveEmojiStyle;
                    case SmileType.Negative:
                        return ConstantsForms.MarkupResources.NegativeEmojiStyle;
                    case SmileType.Neutral:
                        return ConstantsForms.MarkupResources.NeutralEmojiStyle;
                }
                return _style;
            }
            set
            {
                _style = value;
                OnPropertyChanged(nameof(Style));
            }
        }
    }
}