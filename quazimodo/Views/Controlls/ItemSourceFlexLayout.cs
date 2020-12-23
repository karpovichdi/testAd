using quazimodo.Utilities;
using quazimodo.ViewModels;
using Xamarin.Forms;

namespace quazimodo.Views.Controlls
{
    public class ItemSourceFlexLayout : FlexLayout
    {
        public static readonly BindableProperty ItemSourceProperty = 
            BindableProperty.Create(nameof(ItemSource),
            typeof(ObservableRangeCollection<ButtonSmileViewModel>), 
            typeof(ItemSourceFlexLayout), 
            null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanging: null, 
            propertyChanged: OnItemSourceChanged);

        public ObservableRangeCollection<ButtonSmileViewModel> ItemSource
        {
            get => (ObservableRangeCollection<ButtonSmileViewModel>) GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        public ItemSourceFlexLayout()
        {
            
        }

        private static void OnItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ItemSourceFlexLayout container))
            {
                return;
            }

            container.AddButtons(container.ItemSource, container);
        }

        private void AddButtons(
            ObservableRangeCollection<ButtonSmileViewModel> buttonViewModels,
            ItemSourceFlexLayout layout)
        {
            foreach (var viewModel in buttonViewModels)
            {
                var button = new ImageButton
                {
                    BindingContext = viewModel,
                    Command = ((MainViewModel) layout.BindingContext).SongClickCommand
                };

                button.SetBinding(ImageButton.SourceProperty, nameof(viewModel.Image));
                button.SetBinding(ImageButton.IsVisibleProperty, nameof(viewModel.IsVisible));
                button.SetBinding(ImageButton.StyleProperty, nameof(viewModel.Style));
                button.SetBinding(ImageButton.CommandParameterProperty, nameof(viewModel.CommandParameter));
                
                layout.Children.Add(button);
            }
        }
    }
}