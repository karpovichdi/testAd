using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using quazimodo.Models.Enums;
using quazimodo.Services.Interfaces;
using quazimodo.Utilities;
using quazimodo.Utilities.Constants;
using Xamarin.Forms.Internals;

namespace quazimodo.ViewModels
{
    public class SmileItemSourceViewModel : ViewModelBase
    {
        private ISoundService _soundService;
        public ObservableRangeCollection<ButtonSmileViewModel> PositiveItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> NeutralItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> NegativeItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> ItemSource { get; set; }

        public SmileItemSourceViewModel(IEnumerable<ButtonSmileViewModel> list, ISoundService soundService)
        {
            _soundService = soundService;
            
            ItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            
            PositiveItemSource = new ObservableRangeCollection<ButtonSmileViewModel>(); 
            NeutralItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            NegativeItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            
            ItemSource.CollectionChanged += ItemSourceOnCollectionChanged;

            ItemSource.AddRange(list);
            ItemSource.AddRange(GetListOfRecords());
            ItemSource.AddRange(GetPlusButtons());
        }

        private IEnumerable<ButtonSmileViewModel> GetListOfRecords()
        {
            var viewModels = new List<ButtonSmileViewModel>();

            foreach (var soundName in ConstantsForms.SoundParameters)
            {
                var index = ConstantsForms.SoundParameters.IndexOf(soundName);
                var fullPathToFile = Helpers.GetSongPath(soundName);
                if (!File.Exists(fullPathToFile)) continue;
                
                var viewModel = new ButtonSmileViewModel
                {
                    SongPath = fullPathToFile, 
                    IsRecord = true, 
                    CommandParameter = soundName,
                };
                viewModel.SmileType = viewModel.GetSmileTypeByIndex(index);
                viewModels.Add(viewModel);
            }
            
            return viewModels;
        }

        private IEnumerable<ButtonSmileViewModel> GetPlusButtons()
        {
            var viewModels = new List<ButtonSmileViewModel>();

            var countPositive = PositiveItemSource.Count(x => x.IsRecord);
            if (countPositive < ConstantsForms.MaxCountOfSoundInOneTab) 
                viewModels.Add(new ButtonSmileViewModel 
                {
                    IsPlusButton = true, SmileType = SmileType.Positive, 
                    CommandParameter = Helpers.GetSoundParameterByRecordCount(countPositive, SmileType.Positive)
                });
            
            var countNeutral = NeutralItemSource.Count(x => x.IsRecord);
            if (countNeutral < ConstantsForms.MaxCountOfSoundInOneTab) 
                viewModels.Add(new ButtonSmileViewModel
                {
                    IsPlusButton = true, SmileType = SmileType.Neutral, 
                    CommandParameter = Helpers.GetSoundParameterByRecordCount(countNeutral,SmileType.Neutral)
                });
            
            var countNegative = NegativeItemSource.Count(x => x.IsRecord);
            if (countNegative < ConstantsForms.MaxCountOfSoundInOneTab) 
                viewModels.Add(new ButtonSmileViewModel
                {
                    IsPlusButton = true, SmileType = SmileType.Negative, 
                    CommandParameter = Helpers.GetSoundParameterByRecordCount(countNegative, SmileType.Negative)
                });

            return viewModels;
        }

        private void ItemSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    var positive = new List<ButtonSmileViewModel>();
                    var neutral = new List<ButtonSmileViewModel>();
                    var negative = new List<ButtonSmileViewModel>();

                    foreach (ButtonSmileViewModel value in e.NewItems)
                    {
                        switch (value.SmileType)
                        {
                            case SmileType.Positive:
                                positive.Add(value);
                                break;
                            case SmileType.Negative:
                                negative.Add(value);
                                break;
                            case SmileType.Neutral:
                                neutral.Add(value);
                                break;
                        }
                    }
                    
                    PositiveItemSource.AddRange(positive);
                    NeutralItemSource.AddRange(neutral);
                    NegativeItemSource.AddRange(negative);
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var viewModels = e.OldItems.Cast<ButtonSmileViewModel>();
                    foreach (var viewModel in viewModels)
                    {
                        var item = ItemSource.FirstOrDefault(x => x.BindingContext == viewModel);
                        if (item != null)
                        {
                            var itemSource = Helpers.GetSmileItemSourceByType(this, viewModel.SmileType);
                            itemSource.Remove(item);
                        }
                    }
                    
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break; 
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }
    }
}