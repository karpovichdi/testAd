using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using quazimodo.Models.Enums;
using quazimodo.Utilities;
using quazimodo.Utilities.Constants;

namespace quazimodo.ViewModels
{
    public class SmileItemSourceViewModel : ViewModelBase
    {
        public ObservableRangeCollection<ButtonSmileViewModel> PositiveItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> NeutralItemSource { get; set; }
        public ObservableRangeCollection<ButtonSmileViewModel> NegativeItemSource { get; set; }
        
        public ObservableRangeCollection<ButtonSmileViewModel> ItemSource { get; set; }

        public SmileItemSourceViewModel(IEnumerable<ButtonSmileViewModel> list)
        {
            ItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            
            PositiveItemSource = new ObservableRangeCollection<ButtonSmileViewModel>(); 
            NeutralItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            NegativeItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            
            ItemSource.CollectionChanged += ItemSourceOnCollectionChanged;

            UnusedItemsTurnOffVisibility(list);
            
            ItemSource.AddRange(list);
        }

        private void UnusedItemsTurnOffVisibility(IEnumerable<ButtonSmileViewModel> list)
        {
            var recordViewCounter = 0;
            var tabPlusButtonExist = false;
            
            foreach (var item in list)
            {
                if (item.SongPath == null)
                {
                    var fullPathToFile = ResourceHelper.GetSongPath(item.CommandParameter);
                    var fileExist = File.Exists(fullPathToFile);

                    if (fileExist)
                    {
                        item.SongPath = fullPathToFile;
                    }
                }

                if (item.IsRecord)
                {
                    if (string.IsNullOrEmpty(item.SongPath))
                    {
                        if (!tabPlusButtonExist)
                        {
                            item.IsPlusButton = true;
                            item.IsVisible = true;
                            tabPlusButtonExist = true;
                        }
                        else
                        {
                            item.IsVisible = false;
                        }
                    }
                    
                    recordViewCounter++;

                    if (recordViewCounter == ConstantsForms.MaxCountOfSoundInOneTab)
                    {
                        recordViewCounter = 0;
                        tabPlusButtonExist = false;
                    }
                }
                else
                {
                    item.IsVisible = true;
                }
            }
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
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }
    }
}