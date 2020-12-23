using System;
using System.IO;
using System.Reflection;
using quazimodo.Models.Enums;
using quazimodo.Utilities.Constants;

namespace quazimodo.Utilities
{
    public static class ResourceHelper
    {
        public static Stream GetStreamSound(SoundParameter soundName)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream($"{ConstantsForms.PathToSounds}{soundName}{ConstantsForms.SoundExtension}");
        }
        
        public static string GetSongPath(SoundParameter parameter)
        {
            return $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/{parameter}{ConstantsForms.SoundExtension}";
        }
    }
}