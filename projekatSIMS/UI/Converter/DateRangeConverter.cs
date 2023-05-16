using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace projekatSIMS.UI.Converter
{
    public class DateRangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Tuple<DateTime, DateTime> dateTimeRange)
            {
                string formattedRange = $"{dateTimeRange.Item1:dd/MM/yyyy} - {dateTimeRange.Item2:dd/MM/yyyy}";
                return formattedRange;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
