using System;
using System.Globalization;
using System.Windows.Data;

namespace TKomp.Converters
{
    public class ConnectionStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value is ConnectionStateEnum && (ConnectionStateEnum)value == ConnectionStateEnum.Fail))
            {
                return "Red";
            }
            return "Green";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
