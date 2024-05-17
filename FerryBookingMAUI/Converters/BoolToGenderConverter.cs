using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FerryBookingMAUI.Converters
{
    public class BoolToGenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool gender)
            {
                return gender ? "Female" : "Male";
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}