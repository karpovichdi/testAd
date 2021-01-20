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
        private bool _recordsVisible;
        private bool _addButtonVisible;

        public ObservableRangeCollection<ButtonSmileViewModel> PositiveItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> NeutralItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> NegativeItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> RecordsItemSource { get; set; }
        
        public ObservableRangeCollection<ButtonSmileViewModel> ItemSource { get; set; }
        
        public bool RecordsVisible
        {
            get => _recordsVisible;
            set
            {
                _recordsVisible = value;
                OnPropertyChanged(nameof(RecordsVisible));
            }
        }
        
        public bool AddButtonVisible
        {
            get => _addButtonVisible;
            set
            {
                _addButtonVisible = value;
                OnPropertyChanged(nameof(AddButtonVisible));
            }
        }

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
                    Image = $"{soundName}.png"
                };
                viewModels.Add(viewModel);
            }
            
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
                    foreach (ButtonSmileViewModel viewModel in viewModels)
                    {
                        var item = RecordsItemSource.FirstOrDefault(x => x.CommandParameter == viewModel.CommandParameter);
                        if (item != null)
                        {
                            RecordsItemSource.Remove(item);
                        }
                    }
                    
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break; 
                case NotifyCollectionChangedAction.Reset:
                    break;
            }

            RecordsVisible = RecordsItemSource.Count > 0;
            AddButtonVisible = RecordsItemSource.Count < ConstantsForms.MaxCountOfRecords;
        }
    }
}