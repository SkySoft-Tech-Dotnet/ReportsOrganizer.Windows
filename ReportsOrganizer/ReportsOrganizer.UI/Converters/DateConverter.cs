using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ReportsOrganizer.UI.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var date = (int)value;
            return new DateTime().AddHours(date / 3600).AddMinutes(date % 3600);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.Hour * 60 + date.Minute;
        }
    }
}
