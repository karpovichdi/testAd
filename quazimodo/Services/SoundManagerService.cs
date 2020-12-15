using System;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using quazimodo.Constants;
using quazimodo.Enums;
using quazimodo.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace quazimodo.Services
{
    public class SoundManagerService
    {
        private const string SoundExtension = ".mp3";

        internal bool MicrophonePermissionsGranted;

        
        private readonly IAudioService _audioService;
        private readonly AudioPlayer _audioPlayer;
        private readonly AudioRecorderService _recorderService;
        
        public SoundManagerService()
        {
            _recorderService = new AudioRecorderService
            {
                TotalAudioTimeout = TimeSpan.FromSeconds(15)
            };
            
            _audioService = DependencyService.Get<IAudioService>();
            _audioPlayer = new AudioPlayer();
            
            _audioPlayer.FinishedPlaying += AudioFinishHandler;
        }

        public async Task PlaySound(SoundParameter parameter)
        {
            try
            {
                switch (parameter)
                {
                    case SoundParameter.Reject:
                        _audioService.PlayAudioFile(SoundParameter.Reject + SoundExtension);
                        break;
                    case SoundParameter.Accept:
                        _audioService.PlayAudioFile(SoundParameter.Accept + SoundExtension);
                        break;
                    case SoundParameter.Love:
                        _audioService.PlayAudioFile(SoundParameter.Love + SoundExtension);
                        break;
                    case SoundParameter.Crank:
                        _audioService.PlayAudioFile(SoundParameter.Crank + SoundExtension);
                        break;
                    case SoundParameter.Creepypasta:
                        _audioService.PlayAudioFile(SoundParameter.Creepypasta + SoundExtension);
                        break;
                    case SoundParameter.Wow:
                        _audioService.PlayAudioFile(SoundParameter.Wow + SoundExtension);
                        break;
                    case SoundParameter.Star:
                        _audioService.PlayAudioFile(SoundParameter.Star + SoundExtension);
                        break;
                    case SoundParameter.wink:
                        _audioService.PlayAudioFile(SoundParameter.wink + SoundExtension);
                        break;
                    case SoundParameter.thinking:
                        _audioService.PlayAudioFile(SoundParameter.thinking + SoundExtension);
                        break;
                    case SoundParameter.confusingq:
                        _audioService.PlayAudioFile(SoundParameter.confusingq + SoundExtension);
                        break;
                    case SoundParameter.blowkiss:
                        _audioService.PlayAudioFile(SoundParameter.blowkiss + SoundExtension);
                        break;
                    case SoundParameter.grandfather:
                        _audioService.PlayAudioFile(SoundParameter.grandfather + SoundExtension);
                        break;
                    case SoundParameter.sweating:
                        _audioService.PlayAudioFile(SoundParameter.sweating + SoundExtension);
                        break;
                    case SoundParameter.laughingq:
                        _audioService.PlayAudioFile(SoundParameter.laughingq + SoundExtension);
                        break;
                    case SoundParameter.neutral:
                        _audioService.PlayAudioFile(SoundParameter.neutral + SoundExtension);
                        break;
                    case SoundParameter.grinningq:
                        _audioService.PlayAudioFile(SoundParameter.grinningq + SoundExtension);
                        break;
                    case SoundParameter.skull:
                        _audioService.PlayAudioFile(SoundParameter.skull + SoundExtension);
                        break;
                    case SoundParameter.sneezing:
                        _audioService.PlayAudioFile(SoundParameter.sneezing + SoundExtension);
                        break;
                    case SoundParameter.flushed:
                        _audioService.PlayAudioFile(SoundParameter.flushed + SoundExtension);
                        break;
                    case SoundParameter.joy:
                        _audioService.PlayAudioFile(SoundParameter.joy + SoundExtension);
                        break;
                    case SoundParameter.silentq:
                        _audioService.PlayAudioFile(SoundParameter.silentq + SoundExtension);
                        break;
                    case SoundParameter.sad:
                        _audioService.PlayAudioFile(SoundParameter.sad + SoundExtension);
                        break;
                    case SoundParameter.tears:
                        _audioService.PlayAudioFile(SoundParameter.tears + SoundExtension);
                        break;
                    case SoundParameter.amazed:
                        _audioService.PlayAudioFile(SoundParameter.amazed + SoundExtension);
                        break;
                    case SoundParameter.lyingq:
                        _audioService.PlayAudioFile(SoundParameter.lyingq + SoundExtension);
                        break;
                    case SoundParameter.smilingface:
                        _audioService.PlayAudioFile(SoundParameter.smilingface + SoundExtension);
                        break;
                    case SoundParameter.teasing:
                        _audioService.PlayAudioFile(SoundParameter.teasing + SoundExtension);
                        break;
                    case SoundParameter.surprised:
                        _audioService.PlayAudioFile(SoundParameter.surprised + SoundExtension);
                        break;
                    case SoundParameter.dizzy:
                        _audioService.PlayAudioFile(SoundParameter.dizzy + SoundExtension);
                        break;
                    case SoundParameter.angry:
                        _audioService.PlayAudioFile(SoundParameter.angry + SoundExtension);
                        break;
                    case SoundParameter.mouth:
                        _audioService.PlayAudioFile(SoundParameter.mouth + SoundExtension);
                        break;
                    case SoundParameter.alien:
                        _audioService.PlayAudioFile(SoundParameter.alien + SoundExtension);
                        break;
                    case SoundParameter.zanyq:
                        _audioService.PlayAudioFile(SoundParameter.zanyq + SoundExtension);
                        break;
                    case SoundParameter.smile:
                        _audioService.PlayAudioFile(SoundParameter.smile + SoundExtension);
                        break;
                    case SoundParameter.sunglassesq:
                        _audioService.PlayAudioFile(SoundParameter.sunglassesq + SoundExtension);
                        break;
                    case SoundParameter.poop:
                        _audioService.PlayAudioFile(SoundParameter.poop + SoundExtension);
                        break;
                    case SoundParameter.flatmouth:
                        _audioService.PlayAudioFile(SoundParameter.flatmouth + SoundExtension);
                        break;
                    case SoundParameter.eyepatch:
                        _audioService.PlayAudioFile(SoundParameter.eyepatch + SoundExtension);
                        break;
                    case SoundParameter.Laughterq:
                        _audioService.PlayAudioFile(SoundParameter.Laughterq + SoundExtension);
                        break;
                    case SoundParameter.money:
                        _audioService.PlayAudioFile(SoundParameter.money + SoundExtension);
                        break;
                    case SoundParameter.dollar:
                        _audioService.PlayAudioFile(SoundParameter.dollar + SoundExtension);
                        break;
                    case SoundParameter.upset:
                        _audioService.PlayAudioFile(SoundParameter.upset + SoundExtension);
                        break;
                    case SoundParameter.grimacing:
                        _audioService.PlayAudioFile(SoundParameter.grimacing + SoundExtension);
                        break;
                    case SoundParameter.cryd:
                        _audioService.PlayAudioFile(SoundParameter.cryd + SoundExtension);
                        break;
                    case SoundParameter.lol:
                        _audioService.PlayAudioFile(SoundParameter.lol + SoundExtension);
                        break;
                    case SoundParameter.sunglassesqq:
                        _audioService.PlayAudioFile(SoundParameter.sunglassesqq + SoundExtension);
                        break;
                    case SoundParameter.mouthfull:
                        _audioService.PlayAudioFile(SoundParameter.mouthfull + SoundExtension);
                        break;
                    case SoundParameter.edevil:
                        _audioService.PlayAudioFile(SoundParameter.edevil + SoundExtension);
                        break;
                    case SoundParameter.angryface:
                        _audioService.PlayAudioFile(SoundParameter.angryface + SoundExtension);
                        break;
                    case SoundParameter.sleeping:
                        _audioService.PlayAudioFile(SoundParameter.sleeping + SoundExtension);
                        break;
                    case SoundParameter.tongueout:
                        _audioService.PlayAudioFile(SoundParameter.tongueout + SoundExtension);
                        break;
                    case SoundParameter.Laughter:
                        _audioService.PlayAudioFile(SoundParameter.Laughter + SoundExtension);
                        break;
                    case SoundParameter.angel:
                        _audioService.PlayAudioFile(SoundParameter.angel + SoundExtension);
                        break;
                    case SoundParameter.policeman:
                        _audioService.PlayAudioFile(SoundParameter.policeman + SoundExtension);
                        break;
                    case SoundParameter.thief:
                        _audioService.PlayAudioFile(SoundParameter.thief + SoundExtension);
                        break;
                    case SoundParameter.devilsmile:
                        _audioService.PlayAudioFile(SoundParameter.devilsmile + SoundExtension);
                        break;
                    case SoundParameter.disturb:
                        _audioService.PlayAudioFile(SoundParameter.disturb + SoundExtension);
                        break;
                    case SoundParameter.medicalmask:
                        _audioService.PlayAudioFile(SoundParameter.medicalmask + SoundExtension);
                        break;
                    case SoundParameter.expressionless:
                        _audioService.PlayAudioFile(SoundParameter.expressionless + SoundExtension);
                        break;
                    case SoundParameter.pirate:
                        _audioService.PlayAudioFile(SoundParameter.pirate + SoundExtension);
                        break;
                    case SoundParameter.poutine:
                        _audioService.PlayAudioFile(SoundParameter.poutine + SoundExtension);
                        break;
                    case SoundParameter.sadface:
                        _audioService.PlayAudioFile(SoundParameter.sadface + SoundExtension);
                        break;
                    case SoundParameter.snoring:
                        _audioService.PlayAudioFile(SoundParameter.snoring + SoundExtension);
                        break;
                    case SoundParameter.grinninge:
                        _audioService.PlayAudioFile(SoundParameter.grinninge + SoundExtension);
                        break;
                    case SoundParameter.deviltongue:
                        _audioService.PlayAudioFile(SoundParameter.deviltongue + SoundExtension);
                        break;
                    case SoundParameter.closedeyes:
                        _audioService.PlayAudioFile(SoundParameter.closedeyes + SoundExtension);
                        break;
                    case SoundParameter.laughing:
                        _audioService.PlayAudioFile(SoundParameter.laughing + SoundExtension);
                        break;
                    case SoundParameter.grinningy:
                        _audioService.PlayAudioFile(SoundParameter.grinningy + SoundExtension);
                        break;
                    case SoundParameter.ninja:
                        _audioService.PlayAudioFile(SoundParameter.ninja + SoundExtension);
                        break;
                    case SoundParameter.robotq:
                        _audioService.PlayAudioFile(SoundParameter.robotq + SoundExtension);
                        break;
                    case SoundParameter.grinningr:
                        _audioService.PlayAudioFile(SoundParameter.grinningr + SoundExtension);
                        break;
                    case SoundParameter.puke:
                        _audioService.PlayAudioFile(SoundParameter.puke + SoundExtension);
                        break;
                    case SoundParameter.explodes:
                        _audioService.PlayAudioFile(SoundParameter.explodes + SoundExtension);
                        break;
                    case SoundParameter.chilling:
                        _audioService.PlayAudioFile(SoundParameter.chilling + SoundExtension);
                        break;
                    case SoundParameter.sleepingd:
                        _audioService.PlayAudioFile(SoundParameter.sleepingd + SoundExtension);
                        break;
                    case SoundParameter.zipped:
                        _audioService.PlayAudioFile(SoundParameter.zipped + SoundExtension);
                        break;
                    case SoundParameter.death:
                        _audioService.PlayAudioFile(SoundParameter.death + SoundExtension);
                        break;
                    case SoundParameter.teeth:
                        _audioService.PlayAudioFile(SoundParameter.teeth + SoundExtension);
                        break;
                    case SoundParameter.sweattongue:
                        _audioService.PlayAudioFile(SoundParameter.sweattongue + SoundExtension);
                        break;
                    case SoundParameter.kiss:
                        _audioService.PlayAudioFile(SoundParameter.kiss + SoundExtension);
                        break;
                    case SoundParameter.hand:
                        _audioService.PlayAudioFile(SoundParameter.hand + SoundExtension);
                        break;
                    case SoundParameter.frown:
                        _audioService.PlayAudioFile(SoundParameter.frown + SoundExtension);
                        break;
                    case SoundParameter.irritated:
                        _audioService.PlayAudioFile(SoundParameter.irritated + SoundExtension);
                        break;
                    case SoundParameter.crying:
                        _audioService.PlayAudioFile(SoundParameter.crying + SoundExtension);
                        break;
                    case SoundParameter.devilq:
                        _audioService.PlayAudioFile(SoundParameter.devilq + SoundExtension);
                        break;
                    case SoundParameter.hugging:
                        _audioService.PlayAudioFile(SoundParameter.hugging + SoundExtension);
                        break;
                    case SoundParameter.yummy:
                        _audioService.PlayAudioFile(SoundParameter.yummy + SoundExtension);
                        break;
                    case SoundParameter.tasty:
                        _audioService.PlayAudioFile(SoundParameter.tasty + SoundExtension);
                        break;
                    case SoundParameter.airhorn:
                        _audioService.PlayAudioFile(SoundParameter.airhorn + SoundExtension);
                        break;
                    case SoundParameter.chicken:
                        _audioService.PlayAudioFile(SoundParameter.chicken + SoundExtension);
                        break;
                    case SoundParameter.drum:
                        _audioService.PlayAudioFile(SoundParameter.drum + SoundExtension);
                        break;
                    case SoundParameter.fist:
                        _audioService.PlayAudioFile(SoundParameter.fist + SoundExtension);
                        break;
                    case SoundParameter.gun:
                        _audioService.PlayAudioFile(SoundParameter.gun + SoundExtension);
                        break;
                    case SoundParameter.universal:
                        _audioService.PlayAudioFile(SoundParameter.universal + SoundExtension);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void AudioFinishHandler(object sender, EventArgs e)
        {
            _audioPlayer.Play(_recorderService.GetAudioFilePath());
        }

        internal async Task Record()
        {
            if (!_recorderService.IsRecording)
            {
                _recorderService.StartRecording();
            }
            else
            {
                _recorderService.StopRecording();
            }
        }

        internal async Task CheckPermissions()
        {
            var microphoneStatus = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (microphoneStatus != PermissionStatus.Granted)
            {
                await GetPermissions();
            }
            else
            {
                MicrophonePermissionsGranted = true;
            }
        }

        private async Task GetPermissions()
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();
        }

        public async Task StopPlaying()
        {
            _audioService.StopPlaying();
        }
    }
}