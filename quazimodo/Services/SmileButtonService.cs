using System.Collections.Generic;
using System.Threading.Tasks;
using quazimodo.Enums;
using quazimodo.Models;

namespace quazimodo.Services
{
    public class SmileButtonService
    {
        private const string ImageExtension = ".png";

        internal async Task<List<ButtonSmileModel>> GetNeutralModels()
        {
            var list = new List<ButtonSmileModel>()
            {
                new ButtonSmileModel
                {
                    Image = SoundParameter.flatmouth + ImageExtension,
                    CommandParameter = SoundParameter.flatmouth,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.neutral + ImageExtension,
                    CommandParameter = SoundParameter.neutral,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.upset + ImageExtension,
                    CommandParameter = SoundParameter.upset,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.zanyq + ImageExtension,
                    CommandParameter = SoundParameter.zanyq,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.grimacing + ImageExtension,
                    CommandParameter = SoundParameter.grimacing,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.sleeping + ImageExtension,
                    CommandParameter = SoundParameter.sleeping,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.amazed + ImageExtension,
                    CommandParameter = SoundParameter.amazed,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.ninja + ImageExtension,
                    CommandParameter = SoundParameter.ninja,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.robot + ImageExtension,
                    CommandParameter = SoundParameter.robotq,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.skull + ImageExtension,
                    CommandParameter = SoundParameter.skull,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = "ghost" + ImageExtension,
                    CommandParameter = SoundParameter.Creepypasta,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.airhorn + ImageExtension,
                    CommandParameter = SoundParameter.airhorn,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.chicken + ImageExtension,
                    CommandParameter = SoundParameter.chicken,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.drum + ImageExtension,
                    CommandParameter = SoundParameter.drum,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.fist + ImageExtension,
                    CommandParameter = SoundParameter.fist,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.gun + ImageExtension,
                    CommandParameter = SoundParameter.gun,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.universal + ImageExtension,
                    CommandParameter = SoundParameter.universal,
                    SmileType = SmileType.Neutral
                },
            };

            return list;
        }
        
        internal async Task<List<ButtonSmileModel>> GetNegativeModels()
        {
            var list = new List<ButtonSmileModel>()
            {
                new ButtonSmileModel
                {
                    Image = SoundParameter.poutine + ImageExtension,
                    CommandParameter = SoundParameter.poutine,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.sadface + ImageExtension,
                    CommandParameter = SoundParameter.sadface,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.confusingq + ImageExtension,
                    CommandParameter = SoundParameter.confusingq,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = "silent" + ImageExtension,
                    CommandParameter = SoundParameter.silentq,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.death + ImageExtension,
                    CommandParameter = SoundParameter.death,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.disturb + ImageExtension,
                    CommandParameter = SoundParameter.disturb,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.sad + ImageExtension,
                    CommandParameter = SoundParameter.sad,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.mouthfull + ImageExtension,
                    CommandParameter = SoundParameter.mouthfull,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = "lying" + ImageExtension,
                    CommandParameter = SoundParameter.lyingq,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.flushed + ImageExtension,
                    CommandParameter = SoundParameter.flushed,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.zipped + ImageExtension,
                    CommandParameter = SoundParameter.zipped,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.medicalmask + ImageExtension,
                    CommandParameter = SoundParameter.medicalmask,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.sneezing + ImageExtension,
                    CommandParameter = SoundParameter.sneezing,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.thinking + ImageExtension,
                    CommandParameter = SoundParameter.thinking,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.tears + ImageExtension,
                    CommandParameter = SoundParameter.tears,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.snoring + ImageExtension,
                    CommandParameter = SoundParameter.snoring,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.crying + ImageExtension,
                    CommandParameter = SoundParameter.crying,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.sweattongue + ImageExtension,
                    CommandParameter = SoundParameter.sweattongue,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.teasing + ImageExtension,
                    CommandParameter = SoundParameter.teasing,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.angryface + ImageExtension,
                    CommandParameter = SoundParameter.angryface,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.frown + ImageExtension,
                    CommandParameter = SoundParameter.frown,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.irritated + ImageExtension,
                    CommandParameter = SoundParameter.irritated,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.surprised + ImageExtension,
                    CommandParameter = SoundParameter.surprised,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.dizzy + ImageExtension,
                    CommandParameter = SoundParameter.dizzy,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = "shock" + ImageExtension,
                    CommandParameter = SoundParameter.Wow,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.angry + ImageExtension,
                    CommandParameter = SoundParameter.angry,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.puke + ImageExtension,
                    CommandParameter = SoundParameter.puke,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.explodes + ImageExtension,
                    CommandParameter = SoundParameter.explodes,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.chilling + ImageExtension,
                    CommandParameter = SoundParameter.chilling,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.edevil + ImageExtension,
                    CommandParameter = SoundParameter.edevil,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.devilq + ImageExtension,
                    CommandParameter = SoundParameter.devilq,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.expressionless + ImageExtension,
                    CommandParameter = SoundParameter.expressionless,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.deviltongue + ImageExtension,
                    CommandParameter = SoundParameter.deviltongue,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.cryd + ImageExtension,
                    CommandParameter = SoundParameter.cryd,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.laughingq + ImageExtension,
                    CommandParameter = SoundParameter.laughingq,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.devilsmile + ImageExtension,
                    CommandParameter = SoundParameter.devilsmile,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.mouth + ImageExtension,
                    CommandParameter = SoundParameter.mouth,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.sleepingd + ImageExtension,
                    CommandParameter = SoundParameter.sleepingd,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileModel
                {
                    Image = SoundParameter.poop + ImageExtension,
                    CommandParameter = SoundParameter.poop,
                    SmileType = SmileType.Negative
                },
            };
            
            return list;
        }

        internal async Task<List<ButtonSmileModel>> GetPositiveModels()
        {
            var buttonSmileModels = new List<ButtonSmileModel>();
            return buttonSmileModels;
        }
    }
}