using System;
using System.Globalization;
using System.Windows.Data;

namespace RegExDebug.converter
{
    public class Boolean2ColumnSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 1;
            }
            else if ((bool)value)
            {
                return 1;
            }
            else
            {
                return 5;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
