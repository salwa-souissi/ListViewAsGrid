using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListViewAsGrid.CustomComponents.ListViewAsGrid
{
    class ListGridView : ScrollView
    {
        //  private readonly ScrollView _scrollView;
        private readonly Grid _grid;
        private readonly StackLayout _itemsStackLayout;
        private ICommand _innerSelectedCommand;

        public event EventHandler SelectedItemChanged;

        public ScrollOrientation ListOrientation { get; set; }

        public double Spacing { get; set; }


        public static readonly BindableProperty SelectedCommandProperty =
            BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(ListGridView), null);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(ListGridView), default(IEnumerable<object>), BindingMode.TwoWay, propertyChanged: ItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem", typeof(object), typeof(ListGridView), null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(ListGridView), default(DataTemplate));

        public static readonly BindableProperty RowsNumberProperty =
           BindableProperty.Create("RowsNumber", typeof(int), typeof(ListGridView), defaultValue: 1, propertyChanged: OnRowsNumberChanged, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ColumnsNumberProperty =
            BindableProperty.Create("ColumnsNumber", typeof(int), typeof(ListGridView), defaultValue: 1, propertyChanged: OnColumnsNumberChanged, defaultBindingMode: BindingMode.TwoWay);


        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public int RowsNumber
        {
            get { return (int)GetValue(RowsNumberProperty); }
            set { SetValue(RowsNumberProperty, value); }
        }

        public int ColumnsNumber
        {
            get { return (int)GetValue(ColumnsNumberProperty); }
            set { SetValue(ColumnsNumberProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (ListGridView)bindable;
            itemsLayout.SetItems();
        }

        protected virtual void SetItems()
        {
            var Listitems = new List<View>();
            _itemsStackLayout.Children.Clear();
            _itemsStackLayout.Spacing = Spacing;

            _innerSelectedCommand = new Command<View>(view =>
            {
                SelectedItem = view.BindingContext;
                SelectedItem = null;
            });
            if (ListOrientation == ScrollOrientation.Horizontal)
            {
                _itemsStackLayout.Orientation = StackOrientation.Horizontal;
                Orientation = ScrollOrientation.Horizontal;

            }
            else
                if (ListOrientation == ScrollOrientation.Vertical)
            {
                _itemsStackLayout.Orientation = StackOrientation.Vertical;
                Orientation = ScrollOrientation.Vertical;

            }
            else
                if (ListOrientation == ScrollOrientation.Both)
            {
                Orientation = ScrollOrientation.Both;
            }

            foreach (var item in ItemsSource)
            {
                _itemsStackLayout.Children.Add(GetItemView(item));
            }

            _itemsStackLayout.BackgroundColor = BackgroundColor;
            SelectedItem = null;

            if (ItemsSource == null)
            {
                return;
            }
            else
            {
                if (ItemsSource != null)
                    foreach (var item in _itemsStackLayout.Children)
                    {
                        Listitems.Add(item);
                    }
            }
            var lenght = Listitems.Count;
            int MyColumnsCount = 0;
            int MyCount = 0;
            int MyRowCount = 0;

            if (ListOrientation == ScrollOrientation.Horizontal)
            {
                while (MyCount < lenght)
                {
                    MyRowCount = 0;
                    while (MyRowCount < RowsNumber)
                    {
                        _grid.Children.Add(Listitems[MyCount], MyColumnsCount, MyRowCount);
                        MyRowCount++;
                        if (MyCount < lenght - 1)
                            MyCount++;
                        else
                            return;

                    }
                    _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    MyColumnsCount++;

                }
            }
            else if (ListOrientation == ScrollOrientation.Vertical)
            {
                while (MyCount < lenght)
                {
                    MyColumnsCount = 0;
                    while (MyColumnsCount < ColumnsNumber)
                    {
                        _grid.Children.Add(Listitems[MyCount], MyColumnsCount, MyRowCount);
                        MyColumnsCount++;
                        if (MyCount < lenght - 1)
                            MyCount++;
                        else
                            return;

                    }
                    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    MyRowCount++;
                }
            }
            else
            {
                int insered = 0;
                while (MyCount < lenght)
                {
                    for (int rowCount = 0; rowCount < RowsNumber; rowCount++)
                    {
                        for (int columnCount = 0; columnCount < ColumnsNumber; columnCount++)
                        {
                            _grid.Children.Add(Listitems[MyCount], columnCount, rowCount);
                            if (MyCount < lenght - 1)
                                MyCount++;
                            else
                                return;
                            insered++;
                            if (RowsNumber * ColumnsNumber == insered)
                                return;
                        }
                    }
                }
            }
        }

        protected virtual View GetItemView(object item)
        {
            var content = ItemTemplate.CreateContent();
            var view = content as View;

            if (view == null)
            {
                return null;
            }

            view.BindingContext = item;

            var gesture = new TapGestureRecognizer
            {
                Command = _innerSelectedCommand,
                CommandParameter = view
            };

            AddGesture(view, gesture);

            return view;
        }

        private void AddGesture(View view, TapGestureRecognizer gesture)
        {
            view.GestureRecognizers.Add(gesture);

            var layout = view as Layout<View>;

            if (layout == null)
            {
                return;
            }

            foreach (var child in layout.Children)
            {
                AddGesture(child, gesture);
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsView = (ListGridView)bindable;
            if (newValue == oldValue && newValue != null)
            {
                return;
            }

            itemsView.SelectedItemChanged?.Invoke(itemsView, EventArgs.Empty);

            if (itemsView.SelectedCommand?.CanExecute(newValue) ?? false)
            {
                itemsView.SelectedCommand?.Execute(newValue);
            }
        }

        private static void OnRowsNumberChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ListGridView)bindable).OnRowsNumberChangedImpl();
        }
        protected virtual void OnRowsNumberChangedImpl()
        {
            for (int MyCount = 0; MyCount < RowsNumber; MyCount++)
            {
                _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
        }

        private static void OnColumnsNumberChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ListGridView)bindable).OnColumnsNumberChangedImpl();
        }
        protected virtual void OnColumnsNumberChangedImpl()
        {
            for (int MyCount = 0; MyCount < ColumnsNumber; MyCount++)
            {
                _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
        }

        public ListGridView()
        {
            // _scrollView = new ScrollView();
            _grid = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            _grid.ColumnDefinitions = new ColumnDefinitionCollection();
            _grid.RowDefinitions = new RowDefinitionCollection();
            _itemsStackLayout = new StackLayout
            {
                BackgroundColor = BackgroundColor,
                Padding = Padding,
                Spacing = Spacing,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            _grid.BackgroundColor = BackgroundColor;
            // _scrollView.Content = _itemsStackLayout;
            Content = _grid;
            //    Children.Add(_scrollView);

        }
    }
}