using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Magic_Redone.Simple
{
    public class StrToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value?.ToString();

            if (string.IsNullOrWhiteSpace(strValue))
                return 0;

            if (int.TryParse(strValue, out int result))
                return result;

            return 0;
        }
    }
}
