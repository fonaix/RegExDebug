using System;
using System.Windows;
using System.Windows.Controls;

namespace RegExDebug.customs
{
    public static class TextBoxExtension
    {
        #region SelectionStartProperty
        public static readonly DependencyProperty SelectionStartProperty =
        DependencyProperty.RegisterAttached
        (
            "SelectionStart",
            typeof(int),
            typeof(TextBoxExtension),
            new FrameworkPropertyMetadata(SelectionStartChanged) { BindsTwoWayByDefault = true }
        );
        public static int GetSelectionStart(DependencyObject element)
        {
            if (element is null)
                throw new ArgumentNullException("element");
            return (int)element.GetValue(SelectionStartProperty);
        }
        public static void SetSelectionStart(DependencyObject element, int value)
        {
            if (element is null)
                throw new ArgumentNullException("element");
            element.SetValue(SelectionStartProperty, value);
        }
        private static void SelectionStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textbox = (TextBox)d;
            int oldValue, newValue;
            var oldResult = int.TryParse(e.OldValue.ToString(), out oldValue);
            var newResult = int.TryParse(e.NewValue.ToString(), out newValue);
            if (newResult && oldResult)
            {
                textbox.Focus();
                textbox.CaretIndex = newValue;
                textbox.ScrollToEnd();
                textbox.SelectionStart = newValue;
                textbox.Select(newValue, textbox.SelectionLength);
                //textbox.ScrollToLine(textbox.GetLineIndexFromCharacterIndex(newValue));

            }
        }

        #endregion
        #region SelectionLengthProperty
        public static readonly DependencyProperty SelectionLengthProperty =
        DependencyProperty.RegisterAttached
        (
            "SelectionLength",
            typeof(int),
            typeof(TextBoxExtension),
            new FrameworkPropertyMetadata(SelectionLengthChanged) { BindsTwoWayByDefault = true }
        );
        public static int GetSelectionLength(DependencyObject element)
        {
            if (element is null)
                throw new ArgumentNullException("element");
            return (int)element.GetValue(SelectionLengthProperty);
        }
        public static void SetSelectionLength(DependencyObject element, int value)
        {
            if (element is null)
                throw new ArgumentNullException("element");
            element.SetValue(SelectionLengthProperty, value);
        }
        private static void SelectionLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textbox = (TextBox)d;
            int oldValue, newValue;
            var oldResult = int.TryParse(e.OldValue.ToString(), out oldValue);
            var newResult = int.TryParse(e.NewValue.ToString(), out newValue);
            if (newResult)
            {
                textbox.Focus();
                textbox.CaretIndex = textbox.SelectionStart;
                textbox.ScrollToEnd();
                textbox.SelectionLength = newValue;
                textbox.Select(textbox.SelectionStart, newValue);
                //textbox.ScrollToLine(textbox.GetLineIndexFromCharacterIndex(textbox.SelectionStart));
            }
        }
        #endregion
        #region SelectedTextProperty
        public static readonly DependencyProperty SelectedTextProperty =
        DependencyProperty.RegisterAttached
        (
            "SelectedText",
            typeof(string),
            typeof(TextBoxExtension),
            new FrameworkPropertyMetadata(SelectedTextChanged) { BindsTwoWayByDefault = true }
        );
        public static string GetSelectedText(DependencyObject element)
        {
            if (element is null)
                throw new ArgumentNullException("element");
            return (string)element.GetValue(SelectedTextProperty);
        }
        public static void SetSelectedText(DependencyObject element, string value)
        {
            if (element is null)
                throw new ArgumentNullException("element");
            element.SetValue(SelectedTextProperty, value);
        }
        private static void SelectedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textbox = (TextBox)d;
            if (e.NewValue != e.OldValue)
            {
                textbox.SelectedText = (string)e.NewValue;

            }
        }
        #endregion
    }
}
