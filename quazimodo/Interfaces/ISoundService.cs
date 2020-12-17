using System;
using System.Threading.Tasks;
using quazimodo.Enums;

namespace quazimodo.Interfaces
{
    public abstract class ISoundService
    {
        internal bool MicrophonePermissionsGranted;
        public abstract Action<SoundParameter> SoundReleased { get; set; }
        public abstract Action AppClosed { get; set; }
        public abstract Task CreateSoundPathAndPlay(SoundParameter parameter);
        public abstract Task StopPlayingAll();
        public abstract Task CheckPermissions();
    }
}