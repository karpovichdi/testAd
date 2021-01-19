﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using quazimodo.Models.Enums;
using quazimodo.Utilities.Constants;
using quazimodo.ViewModels;

namespace quazimodo.Utilities
{
    public static class Helpers
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
        
        public static ObservableRangeCollection<ButtonSmileViewModel> GetSmileItemSourceByType(SmileItemSourceViewModel viewModel,
            SmileType smileType)
        {
            switch (smileType)
            {
                case SmileType.Positive: return viewModel.PositiveItemSource;
                case SmileType.Negative: return viewModel.NegativeItemSource;
                case SmileType.Neutral: return viewModel.NeutralItemSource;
                case SmileType.Record: return viewModel.RecordsItemSource;
            }

            return null;
        }
        
        public static ButtonSmileViewModel GetViewModelByParameter(this IEnumerable<ButtonSmileViewModel> itemSource, 
            SoundParameter parameter)
        {
            return itemSource.FirstOrDefault(x => 
                x.CommandParameter == parameter);
        }
        
        public static SmileType GetSmileTypeByIndex(this ButtonSmileViewModel viewModel, int index)
        {
            if (index < 10) return SmileType.Positive;
            if (index < 20) return SmileType.Neutral;
            return index < 30 ? SmileType.Negative : SmileType.NotSet;
        }
        
        public static SoundParameter GetSoundParameterByRecordCount(int index)
        {
            switch (index)
            {
                case 0: return SoundParameter.record1;
                case 1: return SoundParameter.record2;
                case 2: return SoundParameter.record3;
                case 3: return SoundParameter.record4;
                case 4: return SoundParameter.record5;
                case 5: return SoundParameter.record6;
                case 6: return SoundParameter.record7;
                case 7: return SoundParameter.record8;
                case 8: return SoundParameter.record9;
                case 9: return SoundParameter.record10;
                case 10: return SoundParameter.record11;
                case 11: return SoundParameter.record12;
                case 12: return SoundParameter.record13;
                case 13: return SoundParameter.record14;
                case 14: return SoundParameter.record15;
                case 15: return SoundParameter.record16;
                case 16: return SoundParameter.record17;
                case 17: return SoundParameter.record18;
                case 18: return SoundParameter.record19;
                case 19: return SoundParameter.record20;
                case 20: return SoundParameter.record21;
                case 21: return SoundParameter.record22;
                case 22: return SoundParameter.record23;
                case 23: return SoundParameter.record24;
                case 24: return SoundParameter.record25;
                case 25: return SoundParameter.record26;
                case 26: return SoundParameter.record27;
                case 27: return SoundParameter.record28;
                case 28: return SoundParameter.record29;
                case 29: return SoundParameter.record30;
            }

            return SoundParameter.accept;
        }
    }
}