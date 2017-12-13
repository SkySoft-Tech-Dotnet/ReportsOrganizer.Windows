using System;
using MahApps.Metro.Converters;
using System.Globalization;
using System.Windows;
using ReportsOrganizer.UI.Controls;

namespace ReportsOrganizer.UI.Converters
{
    public class FlipToScaleXValueConverter : MarkupConverter
    {
        private static FlipToScaleXValueConverter _instance;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new FlipToScaleXValueConverter());
        }

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is PackIconFlipOrientation flip))
            {
                return DependencyProperty.UnsetValue;
            }

            return flip == PackIconFlipOrientation.Horizontal
                || flip == PackIconFlipOrientation.Both ? -1 : 1;
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
    
    public class FlipToScaleYValueConverter : MarkupConverter
    {
        private static FlipToScaleYValueConverter _instance;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new FlipToScaleYValueConverter());
        }

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is PackIconFlipOrientation flip))
            {
                return DependencyProperty.UnsetValue;
            }
            
            return flip == PackIconFlipOrientation.Vertical
                || flip == PackIconFlipOrientation.Both ? -1 : 1;
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
