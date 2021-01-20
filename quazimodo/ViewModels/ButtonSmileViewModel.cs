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
        private DeleteModeState _deleteModeState;
        private Style _style;
        private ImageSource _image;

        public SoundParameter CommandParameter { get; set; }
        public SmileType SmileType { get; set; }

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
        
        public DeleteModeState DeleteModeState
        {
            get => _deleteModeState;
            set
            {
                _deleteModeState = value;
                OnPropertyChanged(nameof(DeleteModeState));
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
                    case SmileType.Record:
                        return ConstantsForms.MarkupResources.PositiveEmojiStyle;
                }
                
                return _style;
            }
            set
            {
                _style = value;
                OnPropertyChanged(nameof(Style));
            }
        }

        public void SwitchDeleteUIState()
        {
            DeleteModeState = DeleteModeState == DeleteModeState.ModeEnabled ? DeleteModeState.ToDelete : DeleteModeState.ModeEnabled;
        }

        public void TurnOnDeleteMode()
        {
            if (SmileType == SmileType.Record)
            {
                DeleteModeState = DeleteModeState.ModeEnabled;
            }
            else
            {
                DeleteModeState = DeleteModeState.SongDisabled;
            }
        }
        
        public void TurnOffDeleteMode()
        {
            DeleteModeState = DeleteModeState.ModeDisabled;
        }
    }
}