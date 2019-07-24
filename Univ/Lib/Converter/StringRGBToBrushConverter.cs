using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace UnivApp
{
    /// <summary>
    /// A converter that takes in an RGB string such as FF00FF and converts it to a WPF brush
    /// </summary>
    public class StringRGBToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return (Color)ColorConverter.ConvertFromString(value + "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
