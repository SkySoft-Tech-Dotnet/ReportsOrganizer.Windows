using ReportsOrganizer.Models;
using ReportsOrganizer.UI.Controls.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ReportsOrganizer.UI.Controls.Converters
{
    internal sealed class ReportGroupConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Report> source)
            {
                return source.GroupBy(_ => _.Created).Select(_ => new ReportGroupViewModel
                {
                    Date = _.Key,

                    Count = _.Count(),
                    Projects = string.Join(", ", _.Select(group => group.Project.ShortName).Distinct()),
                    Time = _.Sum(group => group.Duration) / 60
                });
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    internal sealed class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime source)
            {

            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

}
