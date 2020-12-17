using System.Collections.Generic;
using System.Windows.Input;
using quazimodo.Enums;
using quazimodo.Models;
using quazimodo.utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quazimodo.Controlls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SmileUiStackLayout : StackLayout
    {
        public static readonly BindableProperty ItemCommandProperty = BindableProperty.Create(nameof(Command), 
            typeof(ICommand), typeof(SmileButtonsFlexLayout), null);

        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource),
            typeof(ObservableRangeCollection<ButtonSmileViewModel>), typeof(SmileUiStackLayout), null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanging: null, propertyChanged: OnItemSourceChanged);

        public ObservableRangeCollection<ButtonSmileViewModel> ItemSource
        {
            get => (ObservableRangeCollection<ButtonSmileViewModel>) GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
        
        public ICommand ItemCommand
        {
            get => (ICommand)GetValue(ItemCommandProperty);
            set => SetValue(ItemCommandProperty, value);
        }

        public SmileUiStackLayout()
        {
            InitializeComponent();
        }

        private static void OnItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is SmileUiStackLayout container))
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
                var positiveList = new List<ButtonSmileViewModel>();
                var neutralList = new List<ButtonSmileViewModel>();
                var negativeList = new List<ButtonSmileViewModel>();
                
                foreach (ButtonSmileViewModel value in newValues)
                {
                    switch (value.SmileType)
                    {
                        case SmileType.Positive:
                            positiveList.Add(value);
                            break;
                        case SmileType.Negative:
                            negativeList.Add(value);
                            break;
                        case SmileType.Neutral:
                            neutralList.Add(value);
                            break;
                    }
                }

                container.positiveFlex.ItemCommand = container.ItemCommand;
                container.negativeFlex.ItemCommand = container.ItemCommand;
                container.neutralFlex.ItemCommand = container.ItemCommand;
                
                container.positiveFlex.ItemSource.AddRange(positiveList);
                container.negativeFlex.ItemSource.AddRange(negativeList);
                container.neutralFlex.ItemSource.AddRange(neutralList);
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