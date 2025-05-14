using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Magic_Redone
{
    public class MultiValueVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;

            string[] allowedValues = parameter.ToString().Split(',');
            string currentValue = value.ToString();

            foreach (string allowed in allowedValues)
            {
                if (currentValue == allowed.Trim())
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
