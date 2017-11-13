using MahApps.Metro.Controls;
using ReportsOrganizer.UI.Extensions;
using System.Windows;

namespace ReportsOrganizer.UI.Helpers
{
    public static class FlyoutHelper
    {
        public static readonly DependencyProperty BackButtonVisibilityProperty =
            DependencyProperty.RegisterAttached("BackButtonVisibility", typeof(Visibility), typeof(FlyoutHelper),
                new FrameworkPropertyMetadata(Visibility.Visible,
                    FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetBackButtonVisibility(Flyout dependencyObject, Visibility value)
        {
            dependencyObject.SetValue(BackButtonVisibilityProperty, value);
        }

        public static Visibility GetBackButtonVisibility(Flyout dependencyObject)
        {
            return dependencyObject.GetValue<Visibility>(BackButtonVisibilityProperty);
        }
    }
}
