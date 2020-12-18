﻿using quazimodo.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace quazimodo.Constants
{
    public static class ConstantsForms
    {
        public const int MaxCountOfSoundInOneTime = 10;
        
        public const string PathToSounds = "quazimodo.Sounds.";
        
        public static class EffectIds
        {
            public static string RoundedLayoutEffectId => $"{nameof(quazimodo)}.{nameof(RoundedLayoutEffect)}";
        }
        
        public static class MarkupResources
        {
            public static Color Red => (Color)Application.Current.Resources["red"];
            public static Color LightYellow => (Color)Application.Current.Resources["lightYellow"];
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