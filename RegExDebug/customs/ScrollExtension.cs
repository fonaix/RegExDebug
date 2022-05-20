using RegExDebug.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace RegExDebug.customs
{
    public static class ScrollExtension
    {
        public static readonly DependencyProperty VScrollChangedProperty =
            DependencyProperty.RegisterAttached(
                "VScrollChanged",
                typeof(RelayCommand<object>),
                typeof(ScrollExtension),
                new PropertyMetadata(VScrollChangedPropertyCallBack)
                );

        public static void SetVScrollChanged(ScrollViewer element, RelayCommand<object> value)
        {
            if (element is null) throw new ArgumentNullException(nameof(element));
            element.SetValue(VScrollChangedProperty, value);
        }

        public static RelayCommand<object> GetVScrollChanged(ScrollViewer element)
        {
            if (element is null) throw new ArgumentNullException(nameof(element));
            return (RelayCommand<object>)element.GetValue(VScrollChangedProperty);
        }
        private static void VScrollChangedPropertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer element = (ScrollViewer)d;
            if (element is null) return;
            element.ScrollChanged += (sender, args) =>
            {
                GetVScrollChanged(element).Execute(new object[] { args, GetVScrollTarget(element) });
                args.Handled = true;
            };
        }

        public static readonly DependencyProperty VScrollTargetProperty =
            DependencyProperty.RegisterAttached(
                "VScrollTarget",
                typeof(ScrollViewer),
                typeof(ScrollExtension),
                null
                );

        public static void SetVScrollTarget(ScrollViewer element, ScrollViewer value)
        {
            if (element is null) throw new ArgumentNullException(nameof(element));
            element.SetValue(VScrollTargetProperty, value);
        }

        public static ScrollViewer GetVScrollTarget(ScrollViewer element)
        {
            if (element is null) throw new ArgumentNullException(nameof(element));
            return (ScrollViewer)element.GetValue(VScrollTargetProperty);
        }
    }
}
