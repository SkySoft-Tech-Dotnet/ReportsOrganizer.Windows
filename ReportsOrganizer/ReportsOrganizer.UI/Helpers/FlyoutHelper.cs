using MahApps.Metro.Controls;
using ReportsOrganizer.UI.Extensions;
using System.Windows;
using System.Windows.Input;

namespace ReportsOrganizer.UI.Helpers
{
    public static class FlyoutHelper
    {
        public static readonly DependencyProperty BackCommandProperty =
            DependencyProperty.RegisterAttached("BackCommand", typeof(ICommand), typeof(FlyoutHelper));

        public static readonly DependencyProperty PageContentProperty =
            DependencyProperty.RegisterAttached("PageContent", typeof(object), typeof(FlyoutHelper));

        public static readonly DependencyProperty PageVisibilityProperty =
            DependencyProperty.RegisterAttached("PageVisibility", typeof(Visibility), typeof(FlyoutHelper));

        public static void SetBackCommand(Flyout dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(BackCommandProperty, value);
        }

        public static ICommand GetBackCommand(Flyout dependencyObject)
        {
            return dependencyObject.GetValue<ICommand>(BackCommandProperty);
        }

        public static void SetPageContent(Flyout dependencyObject, object value)
        {
            dependencyObject.SetValue(PageContentProperty, value);
        }

        public static object GetPageContent(Flyout dependencyObject)
        {
            return dependencyObject.GetValue<object>(PageContentProperty);
        }

        public static void SetPageVisibility(Flyout dependencyObject, Visibility value)
        {
            dependencyObject.SetValue(PageVisibilityProperty, value);
        }

        public static Visibility GetPageVisibility(Flyout dependencyObject)
        {
            return dependencyObject.GetValue<Visibility>(PageVisibilityProperty);
        }
    }
}
