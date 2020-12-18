﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using quazimodo.Constants;
using quazimodo.Enums;
using quazimodo.Models;
using quazimodo.utils;
using Xamarin.Forms;

namespace quazimodo.Controlls
{
    public class SmileButtonsFlexLayout : FlexLayout
    {
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource), 
            typeof(ObservableRangeCollection<ButtonSmileViewModel>), typeof(SmileButtonsFlexLayout), null, defaultBindingMode:BindingMode.TwoWay, 
            propertyChanging: null);

        public static readonly BindableProperty ItemStyleProperty = BindableProperty.Create(nameof(ItemStyle), 
            typeof(Style), typeof(SmileButtonsFlexLayout), null, defaultBindingMode:BindingMode.TwoWay, 
            propertyChanging: null, propertyChanged: null);

        public static readonly BindableProperty ItemCommandProperty = BindableProperty.Create(nameof(Command), 
            typeof(ICommand), typeof(SmileButtonsFlexLayout), null);

        public SmileButtonsFlexLayout()
        {
            ItemSource = new ObservableRangeCollection<ButtonSmileViewModel>();
            ItemSource.CollectionChanged += ItemSourceOnCollectionChanged;
        }

        public ICommand ItemCommand
        {
            get => (ICommand)GetValue(ItemCommandProperty);
            set => SetValue(ItemCommandProperty, value);
        }
        
        public ObservableRangeCollection<ButtonSmileViewModel> ItemSource
        {
            get => (ObservableRangeCollection<ButtonSmileViewModel>)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
        
        public Style ItemStyle
        {
            get => (Style)GetValue(ItemStyleProperty);
            set => SetValue(ItemStyleProperty, value);
        }
        
        private void ItemSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ButtonSmileViewModel firstNotVisibleRecord = null;
                    
                    foreach (ButtonSmileViewModel value in e.NewItems)
                    {
                        var imageButton = new ImageButton
                        {
                            CommandParameter = value.CommandParameter,
                            Style = ItemStyle,
                            Command = ItemCommand,
                            IsVisible = !value.IsRecord || value.IsVisibleRecord,
                        };

                        if (value.IsPlusButton) value.IsPlusButton = false;
                        if (firstNotVisibleRecord == null && value.IsRecord && !value.IsVisibleRecord)
                        {
                            value.IsVisibleRecord = true;
                            value.IsPlusButton = true;
                            
                            firstNotVisibleRecord = value;
                        }

                        imageButton.BindingContext = value;

                        Children.Add(imageButton);
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Children.Clear();
                    break;
            }
        }
    }
}