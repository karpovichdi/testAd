﻿using System.Collections.Generic;
using System.Threading.Tasks;
using quazimodo.Enums;
using quazimodo.Models;

namespace quazimodo.Services
{
    public class SmileButtonService
    {
        private const string ImageExtension = ".png";

        internal async Task<List<ButtonSmileViewModel>> GetNeutralModels()
        {
            var list = new List<ButtonSmileViewModel>()
            {
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.flatmouth + ImageExtension,
                    CommandParameter = SoundParameter.flatmouth,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.neutral + ImageExtension,
                    CommandParameter = SoundParameter.neutral,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.upset + ImageExtension,
                    CommandParameter = SoundParameter.upset,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.zanyq + ImageExtension,
                    CommandParameter = SoundParameter.zanyq,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.grimacing + ImageExtension,
                    CommandParameter = SoundParameter.grimacing,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.sleeping + ImageExtension,
                    CommandParameter = SoundParameter.sleeping,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.amazed + ImageExtension,
                    CommandParameter = SoundParameter.amazed,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.ninja + ImageExtension,
                    CommandParameter = SoundParameter.ninja,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.robot + ImageExtension,
                    CommandParameter = SoundParameter.robotq,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.skull + ImageExtension,
                    CommandParameter = SoundParameter.skull,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = "ghost" + ImageExtension,
                    CommandParameter = SoundParameter.Creepypasta,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.airhorn + ImageExtension,
                    CommandParameter = SoundParameter.airhorn,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.chicken + ImageExtension,
                    CommandParameter = SoundParameter.chicken,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.drum + ImageExtension,
                    CommandParameter = SoundParameter.drum,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.fist + ImageExtension,
                    CommandParameter = SoundParameter.fist,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.gun + ImageExtension,
                    CommandParameter = SoundParameter.gun,
                    SmileType = SmileType.Neutral
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.universal + ImageExtension,
                    CommandParameter = SoundParameter.universal,
                    SmileType = SmileType.Neutral
                },
            };

            return list;
        }
        
        internal async Task<List<ButtonSmileViewModel>> GetNegativeModels()
        {
            var list = new List<ButtonSmileViewModel>()
            {
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.poutine + ImageExtension,
                    CommandParameter = SoundParameter.poutine,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.sadface + ImageExtension,
                    CommandParameter = SoundParameter.sadface,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.confusingq + ImageExtension,
                    CommandParameter = SoundParameter.confusingq,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = "silent" + ImageExtension,
                    CommandParameter = SoundParameter.silentq,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.death + ImageExtension,
                    CommandParameter = SoundParameter.death,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.disturb + ImageExtension,
                    CommandParameter = SoundParameter.disturb,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.sad + ImageExtension,
                    CommandParameter = SoundParameter.sad,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.mouthfull + ImageExtension,
                    CommandParameter = SoundParameter.mouthfull,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = "lying" + ImageExtension,
                    CommandParameter = SoundParameter.lyingq,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.flushed + ImageExtension,
                    CommandParameter = SoundParameter.flushed,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.zipped + ImageExtension,
                    CommandParameter = SoundParameter.zipped,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.medicalmask + ImageExtension,
                    CommandParameter = SoundParameter.medicalmask,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.sneezing + ImageExtension,
                    CommandParameter = SoundParameter.sneezing,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.thinking + ImageExtension,
                    CommandParameter = SoundParameter.thinking,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.tears + ImageExtension,
                    CommandParameter = SoundParameter.tears,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.snoring + ImageExtension,
                    CommandParameter = SoundParameter.snoring,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.crying + ImageExtension,
                    CommandParameter = SoundParameter.crying,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.sweattongue + ImageExtension,
                    CommandParameter = SoundParameter.sweattongue,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.teasing + ImageExtension,
                    CommandParameter = SoundParameter.teasing,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.angryface + ImageExtension,
                    CommandParameter = SoundParameter.angryface,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.frown + ImageExtension,
                    CommandParameter = SoundParameter.frown,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.irritated + ImageExtension,
                    CommandParameter = SoundParameter.irritated,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.surprised + ImageExtension,
                    CommandParameter = SoundParameter.surprised,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.dizzy + ImageExtension,
                    CommandParameter = SoundParameter.dizzy,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = "shock" + ImageExtension,
                    CommandParameter = SoundParameter.Wow,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.angry + ImageExtension,
                    CommandParameter = SoundParameter.angry,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.puke + ImageExtension,
                    CommandParameter = SoundParameter.puke,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.explodes + ImageExtension,
                    CommandParameter = SoundParameter.explodes,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.chilling + ImageExtension,
                    CommandParameter = SoundParameter.chilling,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.edevil + ImageExtension,
                    CommandParameter = SoundParameter.edevil,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.devilq + ImageExtension,
                    CommandParameter = SoundParameter.devilq,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.expressionless + ImageExtension,
                    CommandParameter = SoundParameter.expressionless,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.deviltongue + ImageExtension,
                    CommandParameter = SoundParameter.deviltongue,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.cryd + ImageExtension,
                    CommandParameter = SoundParameter.cryd,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.laughingq + ImageExtension,
                    CommandParameter = SoundParameter.laughingq,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.devilsmile + ImageExtension,
                    CommandParameter = SoundParameter.devilsmile,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.mouth + ImageExtension,
                    CommandParameter = SoundParameter.mouth,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.sleepingd + ImageExtension,
                    CommandParameter = SoundParameter.sleepingd,
                    SmileType = SmileType.Negative
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.poop + ImageExtension,
                    CommandParameter = SoundParameter.poop,
                    SmileType = SmileType.Negative
                },
            };
            
            return list;
        }

        internal async Task<List<ButtonSmileViewModel>> GetPositiveModels()
        {
            var list = new List<ButtonSmileViewModel>()
            {
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.smilingface + ImageExtension,
                    CommandParameter = SoundParameter.smilingface,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.closedeyes + ImageExtension,
                    CommandParameter = SoundParameter.closedeyes,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = "kissing" + ImageExtension,
                    CommandParameter = SoundParameter.teeth,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.hand + ImageExtension,
                    CommandParameter = SoundParameter.hand,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.hugging + ImageExtension,
                    CommandParameter = SoundParameter.hugging,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.angel + ImageExtension,
                    CommandParameter = SoundParameter.angel,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.sweating + ImageExtension,
                    CommandParameter = SoundParameter.sweating,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.joy + ImageExtension,
                    CommandParameter = SoundParameter.joy,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.lol + ImageExtension,
                    CommandParameter = SoundParameter.lol,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.tongueout + ImageExtension,
                    CommandParameter = SoundParameter.tongueout,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.yummy + ImageExtension,
                    CommandParameter = SoundParameter.yummy,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.tasty + ImageExtension,
                    CommandParameter = SoundParameter.tasty,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.blowkiss + ImageExtension,
                    CommandParameter = SoundParameter.blowkiss,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = "hearteyes" + ImageExtension,
                    CommandParameter = SoundParameter.Love,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.Laughter + ImageExtension,
                    CommandParameter = SoundParameter.Laughter,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.wink + ImageExtension,
                    CommandParameter = SoundParameter.wink,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.grinninge + ImageExtension,
                    CommandParameter = SoundParameter.grinninge,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.grinningr + ImageExtension,
                    CommandParameter = SoundParameter.grinningr,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.grinningy + ImageExtension,
                    CommandParameter = SoundParameter.grinningy,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.Laughterq + ImageExtension,
                    CommandParameter = SoundParameter.Laughterq,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.money + ImageExtension,
                    CommandParameter = SoundParameter.money,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = "starred" + ImageExtension,
                    CommandParameter = SoundParameter.Star,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.eyepatch + ImageExtension,
                    CommandParameter = SoundParameter.eyepatch,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = "cool" + ImageExtension,
                    CommandParameter = SoundParameter.Crank,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = "sunglasses" + ImageExtension,
                    CommandParameter = SoundParameter.sunglassesq,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.thief + ImageExtension,
                    CommandParameter = SoundParameter.thief,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.policeman + ImageExtension,
                    CommandParameter = SoundParameter.policeman,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.grandfather + ImageExtension,
                    CommandParameter = SoundParameter.grandfather,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.grinningq + ImageExtension,
                    CommandParameter = SoundParameter.grinningq,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.laughing + ImageExtension,
                    CommandParameter = SoundParameter.laughing,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.kiss + ImageExtension,
                    CommandParameter = SoundParameter.kiss,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.sunglassesq + ImageExtension,
                    CommandParameter = SoundParameter.sunglassesq,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.dollar + ImageExtension,
                    CommandParameter = SoundParameter.dollar,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.smile + ImageExtension,
                    CommandParameter = SoundParameter.smile,
                    SmileType = SmileType.Positive
                },
                new ButtonSmileViewModel
                {
                    Image = SoundParameter.alien + ImageExtension,
                    CommandParameter = SoundParameter.alien,
                    SmileType = SmileType.Positive
                },
            };

            return list;
        }
    }
}