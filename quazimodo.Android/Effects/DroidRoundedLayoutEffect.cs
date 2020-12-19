using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using quazimodo.Droid.Effects;
using quazimodo.Views.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AColor = Android.Graphics.Color;
using AView = Android.Views.View;
using Droid = Android;

[assembly: ResolutionGroupName(nameof(quazimodo))]
[assembly: ExportEffect(typeof(DroidRoundedLayoutEffect), nameof(RoundedLayoutEffect))]
namespace quazimodo.Droid.Effects
{
    public class DroidRoundedLayoutEffect : PlatformEffect
    {
        private RoundedLayoutEffect _effect;
        private Context _context;
        private AView _view;

        protected override void OnAttached()
        {
            _view = Control ?? Container;
            _context = MainActivity.Instance;
            _effect = (RoundedLayoutEffect)Element.Effects.First(x => x is RoundedLayoutEffect);
            _effect.PropertyChanged += EffectPropertyChanged;

            SetStyle(_view);
        }

        protected override void OnDetached()
        {
            _effect.PropertyChanged -= EffectPropertyChanged;
        }

        private void EffectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_effect.HasBorder))
            {
                if (_effect.HasBorder)
                    SetBorder(_view);
                else if (_view.Background is GradientDrawable drawable)
                    drawable.SetStroke(0, AColor.Transparent);
            }
            else if (e.PropertyName == nameof(_effect.BorderColor) && _effect.HasBorder)
            {
                SetBorder(_view);
            }
        }

        private void SetStyle(AView view)
        {
            if (view == null) return;

            view.OutlineProvider = new RoundedOutlineProvider(_context.ToPixels(_effect.CornerRadius));
            view.ClipToOutline = true;

            if (_effect.HasShadow)
                view.Elevation = _context.ToPixels(_effect.ShadowRadius);

            if (_effect.HasBorder)
                SetBorder(view);
        }

        private void SetBorder(AView view)
        {
            var gradientDrawable = new GradientDrawable();

            if (Element is VisualElement visualElement)
                gradientDrawable.SetColor(visualElement.BackgroundColor.ToAndroid());

            gradientDrawable.SetShape(ShapeType.Rectangle);
            gradientDrawable.SetCornerRadius(_context.ToPixels(_effect.CornerRadius));
            gradientDrawable.SetStroke((int)_context.ToPixels(_effect.BorderWidth), AColor.ParseColor(_effect.BorderColor.ToHex()));

            view.SetBackground(gradientDrawable);
        }

        public class RoundedOutlineProvider : ViewOutlineProvider
        {
            private readonly float _radius;

            public RoundedOutlineProvider(float radius)
            {
                _radius = radius;
            }

            public override void GetOutline(AView view, Outline outline)
            {
                outline?.SetRoundRect(0, 0, view.Width, view.Height, _radius);
            }
        }
    }
}