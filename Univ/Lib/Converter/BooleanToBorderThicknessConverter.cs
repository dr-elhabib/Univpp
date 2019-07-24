using System;
using System.Globalization;
using System.Windows;

namespace Univ
{
    /// <summary>
    /// A converter that takes in a boolean and returns a thickness of 2 if true, useful for applying 
    /// border radius on a true value
    /// </summary>
    public class BooleanToBorderThicknessConverter : BaseValueConverter<BooleanToBorderThicknessConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MessageBox.Show((Boolean)value+" ");
             return true;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
