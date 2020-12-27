using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
            
            container.ItemSource.CollectionChanged -= container.ItemSourceOnCollectionChanged;
            container.ItemSource.CollectionChanged += container.ItemSourceOnCollectionChanged;
        }

        private void ItemSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IEnumerable<ButtonSmileViewModel> viewModels;
            
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    viewModels = e.NewItems.Cast<ButtonSmileViewModel>();
                    foreach (var viewModel in viewModels)
                    {
                        var newButton = new ImageButton
                        {
                            BindingContext = viewModel,
                            Command = ((MainViewModel) BindingContext).SongClickCommand
                        };

                        newButton.SetBinding(ImageButton.SourceProperty, nameof(viewModel.Image));
                        newButton.SetBinding(ImageButton.StyleProperty, nameof(viewModel.Style));
                        newButton.SetBinding(ImageButton.CommandParameterProperty, nameof(viewModel.CommandParameter));
                        
                        Children.Add(newButton);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    viewModels = e.OldItems.Cast<ButtonSmileViewModel>();
                    foreach (var viewModel in viewModels)
                    {
                        var button = Children.FirstOrDefault(x => x.BindingContext == viewModel);
                        if (button != null) Children.Remove(button);
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
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
                button.SetBinding(ImageButton.StyleProperty, nameof(viewModel.Style));
                button.SetBinding(ImageButton.CommandParameterProperty, nameof(viewModel.CommandParameter));
                
                layout.Children.Add(button);
            }
        }
    }
}