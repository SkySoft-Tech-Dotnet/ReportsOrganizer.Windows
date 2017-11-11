using System.Windows;

namespace ReportsOrganizer.UI.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static TProperty GetValue<TProperty>(
            this DependencyObject dependencyObject, DependencyProperty dependencyProperty)
        {
            return (TProperty)dependencyObject.GetValue(dependencyProperty);
        }
    }
}
