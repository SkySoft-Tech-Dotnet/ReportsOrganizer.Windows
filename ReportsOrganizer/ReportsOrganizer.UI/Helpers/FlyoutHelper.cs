using MahApps.Metro.Controls;
using ReportsOrganizer.UI.Extensions;
using System.Windows;
using System.Windows.Input;

namespace ReportsOrganizer.UI.Helpers
{
    public static class FlyoutHelper
    {
        public static readonly DependencyProperty BackButtonVisibilityProperty =
            DependencyProperty.RegisterAttached("BackButtonVisibility", typeof(Visibility), typeof(FlyoutHelper),
                new FrameworkPropertyMetadata(Visibility.Visible,
                    FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty BackCommandProperty =
            DependencyProperty.RegisterAttached("BackCommand", typeof(ICommand), typeof(FlyoutHelper));

        public static void SetBackButtonVisibility(Flyout dependencyObject, Visibility value)
        {
            dependencyObject.SetValue(BackButtonVisibilityProperty, value);
        }

        public static Visibility GetBackButtonVisibility(Flyout dependencyObject)
        {
            return dependencyObject.GetValue<Visibility>(BackButtonVisibilityProperty);
        }

        public static void SetBackCommand(Flyout dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(BackCommandProperty, value);
        }

        public static ICommand GetBackCommand(Flyout dependencyObject)
        {
            return dependencyObject.GetValue<ICommand>(BackCommandProperty);
        }
    }
}
