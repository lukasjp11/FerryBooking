using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FerryBookingMAUI.Converters
{
    public class MaxCarsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int currentCount && parameter is int maxCount)
            {
                return $"{currentCount} / {maxCount}";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}