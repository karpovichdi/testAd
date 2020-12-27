using quazimodo.Models.Enums;
using quazimodo.Views.Effects;
using Xamarin.Forms;

namespace quazimodo.Utilities.Constants
{
    public static class ConstantsForms
    {
        public const string ImageExtension = ".png";
        public const string SoundExtension = ".mp3";

        public const int MaxCountOfSoundInOneTime = 10;
        public const int MaxCountOfSoundInOneTab = 10;
        
        public static int AdditionalValueToRecordProgress = (100/MaxLenghtOfRecordedSoundInSecond)/2;

        public const string PathToSounds = "quazimodo.Sounds.";
        public const string Positive = "Positive";
        public const string Negative = "Negative";

        public static SoundParameter[] SoundParameters = {
            SoundParameter.record1,
            SoundParameter.record2,
            SoundParameter.record3,
            SoundParameter.record4,
            SoundParameter.record5,
            SoundParameter.record6,
            SoundParameter.record7,
            SoundParameter.record8,
            SoundParameter.record9,
            SoundParameter.record10,
            SoundParameter.record11,
            SoundParameter.record12,
            SoundParameter.record13,
            SoundParameter.record14,
            SoundParameter.record15,
            SoundParameter.record16,
            SoundParameter.record17,
            SoundParameter.record18,
            SoundParameter.record19,
            SoundParameter.record20,
            SoundParameter.record21,
            SoundParameter.record22,
            SoundParameter.record23,
            SoundParameter.record24,
            SoundParameter.record25,
            SoundParameter.record26,
            SoundParameter.record27,
            SoundParameter.record28,
            SoundParameter.record29,
            SoundParameter.record30
        };

        public static FontImageSource DefaultImageSource => new FontImageSource()
        {
            FontFamily = MarkupResources.IcoMoonFontForAndroid,
            Size = 44,
            Color = Color.Red,
            Glyph = FontIcons.mic
        };
        
        public static class EffectIds
        {
            public static string RoundedLayoutEffectId => $"{nameof(quazimodo)}.{nameof(RoundedLayoutEffect)}";
        }
        
        public static class MarkupResources
        {
            public static double ProgressViewSize => (double)Application.Current.Resources["progressViewSize"];
            public static Color DarkPurple => (Color)Application.Current.Resources["darkPurple"];
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