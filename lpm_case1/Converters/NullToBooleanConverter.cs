using System;
using System.Globalization;
using System.Windows.Data;

namespace lpm_case1.Converters
{
    public class NullToBooleanConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter as string == "Invert")
            {
                return value == null;
            }

        
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}