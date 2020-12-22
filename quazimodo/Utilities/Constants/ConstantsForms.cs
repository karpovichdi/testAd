using quazimodo.Views.Effects;
using Xamarin.Forms;

namespace quazimodo.Utilities.Constants
{
    public static class ConstantsForms
    {
        public const string ImageExtension = ".png";
        
        public const int MaxCountOfSoundInOneTime = 10;
        
        public static int AdditionalValueToRecordProgress = (100/MaxLenghtOfRecordedSoundInSecond)/2;

        public const string PathToSounds = "quazimodo.Sounds.";
        public const string Positive = "Positive";
        public const string Negative = "Negative";
        
        public static class EffectIds
        {
            public static string RoundedLayoutEffectId => $"{nameof(quazimodo)}.{nameof(RoundedLayoutEffect)}";
        }
        
        public static class MarkupResources
        {
            public static Color Red => (Color)Application.Current.Resources["red"];
            public static Color LightYellow => (Color)Application.Current.Resources["lightYellow"];
            public static Color Gray => (Color)Application.Current.Resources["grafit"];
            public static Color White7 => (Color)Application.Current.Resources["white7"];
            public static Style PositiveEmojiStyle => (Style)Application.Current.Resources["positiveEmojiItem"];
            public static Style NeutralEmojiStyle => (Style)Application.Current.Resources["neutralEmojiItem"];
            public static Style NegativeEmojiStyle => (Style)Application.Current.Resources["negativeEmojiItem"];

            public static string IcoMoonFontForAndroid =>
                (string) ((OnPlatform<string>) Application.Current.Resources["icomoonFont"]).Platforms[0].Value;
                
            public static string IcoMoonFontForIos =>
                (string) ((OnPlatform<string>) Application.Current.Resources["icomoonFont"]).Platforms[1].Value;

        }

        public const int MaxLenghtOfRecordedSoundInSecond = 10;
    }
}