using System.Globalization;
using System.Windows.Data;

namespace Zadanie2Lista2
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && (string)value == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Boolean && (Boolean)value)
            {
                return "";
            }
            else
            {
                return "";
            }
        }
    }
}