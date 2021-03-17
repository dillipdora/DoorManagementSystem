using System;
using System.Globalization;
using System.Windows.Data;

namespace DoorManagement.Converters
{
    public class LockStatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? new Uri("pack://application:,,,/DoorManagement;component/images/Locked.png")
                               : new Uri("pack://application:,,,/DoorManagement;component/images/UnLocked.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
