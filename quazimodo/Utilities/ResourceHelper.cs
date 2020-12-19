using System.IO;
using System.Reflection;
using quazimodo.Utilities.Constants;

namespace quazimodo.Utilities
{
    public static class ResourceHelper
    {
        public static Stream GetStreamSound(string soundName)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream(ConstantsForms.PathToSounds + soundName);
        }
    }
}