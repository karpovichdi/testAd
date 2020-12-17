using quazimodo.Effects;

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
        
        public static class MessagingCenterConstants
        {
            public static string LastSongFinished => "LastSongFinished";
        }
    }
}