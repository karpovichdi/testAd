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
            var buttonSmileModels = new List<ButtonSmileModel>();
            return buttonSmileModels;
        }

        internal async Task<List<ButtonSmileModel>> GetPositiveModels()
        {
            var buttonSmileModels = new List<ButtonSmileModel>();
            return buttonSmileModels;
        }
    }
}