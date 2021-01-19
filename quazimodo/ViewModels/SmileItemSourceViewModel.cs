using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using quazimodo.Models.Enums;
using quazimodo.Services.Interfaces;
using quazimodo.Utilities;
using quazimodo.Utilities.Constants;

namespace quazimodo.ViewModels
{
    public class SmileItemSourceViewModel : ViewModelBase
    {
        private ISoundService _soundService;

        public ObservableRangeCollection<ButtonSmileViewModel> PositiveItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> NeutralItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> NegativeItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> RecordsItemSource { get; set; }
        
        public ObservableRangeCollection<ButtonSmileViewModel> ItemSource { get; set; }

        public SmileItemSourceViewModel(IEnumerable<ButtonSmileViewModel> list, ISoundService soundService)
        {
            _soundService = soundService;
            
            ItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            
            PositiveItemSource = new ObservableRangeCollection<ButtonSmileViewModel>(); 
            NeutralItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            NegativeItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            RecordsItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            
            ItemSource.CollectionChanged += ItemSourceOnCollectionChanged;

            ItemSource.AddRange(list);
            ItemSource.AddRange(GetListOfRecords());
            ItemSource.AddRange(GetPlusButton());
        }
        
        private IEnumerable<ButtonSmileViewModel> GetListOfRecords()
        {
            var viewModels = new List<ButtonSmileViewModel>();

            foreach (var soundName in ConstantsForms.SoundParameters)
            {
                var fullPathToFile = Helpers.GetSongPath(soundName);
                if (!File.Exists(fullPathToFile)) continue;

                var viewModel = new ButtonSmileViewModel
                {
                    SongPath = fullPathToFile,
                    CommandParameter = soundName,
                    SmileType = SmileType.Record,
                };
                viewModels.Add(viewModel);
            }
            
            return viewModels;
        }

        private IEnumerable<ButtonSmileViewModel> GetPlusButton()
        {
            var viewModels = new List<ButtonSmileViewModel>();

            var recordCount = RecordsItemSource.Count(x => x.SmileType == SmileType.Record);
            if (recordCount < ConstantsForms.MaxCountOfRecords) 
                viewModels.Add(new ButtonSmileViewModel
                {
                    IsPlusButton = true, SmileType = SmileType.Record, 
                    CommandParameter = Helpers.GetSoundParameterByRecordCount(recordCount)
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
                    var records = new List<ButtonSmileViewModel>();

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
                            case SmileType.Record:
                                records.Add(value);
                                break;
                        }
                    }
                    
                    PositiveItemSource.AddRange(positive);
                    NeutralItemSource.AddRange(neutral);
                    NegativeItemSource.AddRange(negative);
                    RecordsItemSource.AddRange(records);
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