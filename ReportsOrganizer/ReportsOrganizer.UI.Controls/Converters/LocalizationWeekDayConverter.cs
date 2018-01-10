using ReportsOrganizer.Localization.Helpers;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ReportsOrganizer.UI.Controls.Converters
{
    internal sealed class LocalizationWeekDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DayOfWeek source)
            {
                switch (source)
                {
                    case DayOfWeek.Sunday:
                        return LocalizationHelper.GetLocalizedValue("General:Day_Sunday_Short");
                    case DayOfWeek.Monday:
                        return LocalizationHelper.GetLocalizedValue("General:Day_Monday_Short");
                    case DayOfWeek.Tuesday:
                        return LocalizationHelper.GetLocalizedValue("General:Day_Tuesday_Short");
                    case DayOfWeek.Wednesday:
                        return LocalizationHelper.GetLocalizedValue("General:Day_Wednesday_Short");
                    case DayOfWeek.Thursday:
                        return LocalizationHelper.GetLocalizedValue("General:Day_Thursday_Short");
                    case DayOfWeek.Friday:
                        return LocalizationHelper.GetLocalizedValue("General:Day_Friday_Short");
                    case DayOfWeek.Saturday:
                        return LocalizationHelper.GetLocalizedValue("General:Day_Saturday_Short");
                }
            }
            return default(string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
