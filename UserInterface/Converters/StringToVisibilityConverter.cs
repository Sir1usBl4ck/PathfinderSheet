using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UserInterface.Converters
{
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                var stringValue = value as string;

                if (!String.IsNullOrEmpty(stringValue))
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
