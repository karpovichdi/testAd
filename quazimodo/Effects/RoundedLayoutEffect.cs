using System.ComponentModel;
using System.Runtime.CompilerServices;
using quazimodo.Constants;
using Xamarin.Forms;

namespace quazimodo.Effects
{
    public class RoundedLayoutEffect : RoutingEffect, INotifyPropertyChanged
    {
        private bool _hasBorder;
        private Color _borderColor;

        public bool HasBorder
        {
            get => _hasBorder;
            set
            {
                if (value != _hasBorder)
                {
                    _hasBorder = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public Color BorderColor
        {
            get => _borderColor;
            set 
            {
                if (value != _borderColor)
                {
                    _borderColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasShadow { get; set; } = true;

        public float CornerRadius { get; set; } = 10;

        public float ShadowRadius { get; set; } = 3;

        public double BorderWidth { get; set; } = 10;

        public Color ShadowColor { get; set; } = Color.FromRgba(0, 0, 0, 0.7f);


        public event PropertyChangedEventHandler PropertyChanged;

        public RoundedLayoutEffect()
            : base(ConstantsForms.EffectIds.RoundedLayoutEffectId)
        {
            _borderColor = Color.FromRgba(0, 0, 0, 0.7f);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}