using System;
using System.Threading.Tasks;
using quazimodo.Models.Enums;
using Xamarin.Essentials;

namespace quazimodo.Services.Interfaces
{
    public abstract class ISoundService
    {
        internal bool MicrophonePermissionsGranted;
        public abstract Action<SoundParameter> SoundReleased { get; set; }
        public abstract Action AppClosed { get; set; }
        public abstract Action RecordReleased { get; set; }
        public abstract Task CreateSoundPathAndPlay(SoundParameter parameter);
        public abstract Task StopPlayingAll();
        public abstract Task<PermissionStatus> CheckPermissions();
        public abstract Task StartRecording(SoundParameter commandParameter);
        public abstract Task StopRecording();
    }
}