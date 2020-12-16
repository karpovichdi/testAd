using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using quazimodo.Enums;
using quazimodo.Models;
using quazimodo.utils;
using quazimodo.ViewModels;
using Xamarin.Forms;

namespace quazimodo.Controlls
{
    public class SmileButtonsFlexLayout : FlexLayout
    {
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource), 
            typeof(ObservableRangeCollection<ButtonSmileViewModel>), typeof(SmileButtonsFlexLayout), null, defaultBindingMode:BindingMode.TwoWay, 
            propertyChanging: null, propertyChanged: OnPreferredTransportChanged);
        
        public static readonly BindableProperty ItemStyleProperty = BindableProperty.Create(nameof(ItemStyle), 
            typeof(Style), typeof(SmileButtonsFlexLayout), null, defaultBindingMode:BindingMode.TwoWay, 
            propertyChanging: null, propertyChanged: null);

        public static readonly BindableProperty ItemCommandProperty = BindableProperty.Create(nameof(Command), 
            typeof(ICommand), typeof(SmileButtonsFlexLayout), null);
        
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
        
        private static void OnPreferredTransportChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is SmileButtonsFlexLayout container))
            {
                return;
            }

            ObservableRangeCollection<ButtonSmileViewModel> newValues = null;
            ObservableRangeCollection<ButtonSmileViewModel> oldValues = null;

            if (newValue != null) newValues = newValue as ObservableRangeCollection<ButtonSmileViewModel>;
            if (oldValue != null) oldValues = oldValue as ObservableRangeCollection<ButtonSmileViewModel>;

            var newValuesCount = 0;
            var oldValuesCount = 0;

            if (newValues != null) newValuesCount = newValues.Count;
            if (oldValues != null) oldValuesCount = oldValues.Count;

            if (newValuesCount > oldValuesCount)
            {
                foreach (ButtonSmileViewModel value in newValues)
                {
                    var imageButton = new ImageButton
                    {
                        Source = value.Image,
                        CommandParameter = value.CommandParameter,
                        Style = container.ItemStyle,
                        BindingContext = value,
                        Command = container.ItemCommand
                    };

                    container.Children.Add(imageButton);
                }   
            }
            else if (newValuesCount < oldValuesCount)
            {
                container.Children.Clear();
            }
            else
            {
                
            }
        }
    }
}