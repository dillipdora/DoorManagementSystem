using System;
using System.Globalization;
using System.Windows.Data;

namespace DoorManagement.Converters
{
    public class OpenStatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? new Uri("pack://application:,,,/DoorManagement;component/images/Opened.png")
                               : new Uri("pack://application:,,,/DoorManagement;component/images/Closed.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
