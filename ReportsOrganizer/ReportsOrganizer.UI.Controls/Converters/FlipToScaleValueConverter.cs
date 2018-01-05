using MahApps.Metro.Converters;
using System;
using System.Globalization;
using System.Windows;

namespace ReportsOrganizer.UI.Controls.Converters
{
    internal class FlipToScaleXValueConverter : MarkupConverter
    {
        private static FlipToScaleXValueConverter _instance;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new FlipToScaleXValueConverter());
        }

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IconFlipOrientation flip))
            {
                return DependencyProperty.UnsetValue;
            }

            return flip == IconFlipOrientation.Horizontal
                || flip == IconFlipOrientation.Both ? -1 : 1;
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    internal class FlipToScaleYValueConverter : MarkupConverter
    {
        private static FlipToScaleYValueConverter _instance;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new FlipToScaleYValueConverter());
        }

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IconFlipOrientation flip))
            {
                return DependencyProperty.UnsetValue;
            }

            return flip == IconFlipOrientation.Vertical
                || flip == IconFlipOrientation.Both ? -1 : 1;
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
