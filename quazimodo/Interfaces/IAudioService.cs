using System;

namespace quazimodo.Interfaces
{
    public interface IAudioService
    {
        void PlayAudioFile(string fileName);

        void StopPlaying();
    }
}