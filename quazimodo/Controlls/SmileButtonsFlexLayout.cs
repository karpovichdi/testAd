using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using quazimodo.Enums;
using quazimodo.Models;
using quazimodo.utils;
using Xamarin.Forms;

namespace quazimodo.Controlls
{
    public class SmileButtonsFlexLayout : FlexLayout
    {
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource), 
            typeof(ObservableRangeCollection<ButtonSmileModel>), typeof(SmileButtonsFlexLayout), null, defaultBindingMode:BindingMode.TwoWay, 
            propertyChanging: null, propertyChanged: OnPreferredTransportChanged);
        
        public static readonly BindableProperty ItemStyleProperty = BindableProperty.Create(nameof(ItemStyle), 
            typeof(Style), typeof(SmileButtonsFlexLayout), null, defaultBindingMode:BindingMode.TwoWay, 
            propertyChanging: null, propertyChanged: null);

        public ObservableRangeCollection<ButtonSmileModel> ItemSource
        {
            get => (ObservableRangeCollection<ButtonSmileModel>)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
        
        public Style ItemStyle
        {
            get => (Style)GetValue(ItemStyleProperty);
            set => SetValue(ItemStyleProperty, value);
        }

        public SmileButtonsFlexLayout()
        {
            
        }
        
        private static void OnPreferredTransportChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is SmileButtonsFlexLayout container))
            {
                return;
            }

            ObservableRangeCollection<ButtonSmileModel> newValues = null;
            ObservableRangeCollection<ButtonSmileModel> oldValues = null;

            if (newValue != null) newValues = newValue as ObservableRangeCollection<ButtonSmileModel>;
            if (oldValue != null) oldValues = oldValue as ObservableRangeCollection<ButtonSmileModel>;

            var newValuesCount = 0;
            var oldValuesCount = 0;

            if (newValues != null) newValuesCount = newValues.Count;
            if (oldValues != null) oldValuesCount = oldValues.Count;

            if (newValuesCount > oldValuesCount)
            {
                foreach (ButtonSmileModel value in newValues)
                {
                    var imageButton = new ImageButton
                    {
                        Source = value.Image,
                        CommandParameter = value.CommandParameter,
                        Style = container.ItemStyle
                    };

                    container.Children.Add(imageButton);
                }   
            }
            else if (newValuesCount < oldValuesCount)
            {
                
            }
            else
            {
                
            }
        }
    }
}