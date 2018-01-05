using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ReportsOrganizer.UI.Controls.Helpers
{
    public static class ExpanderHelper
    {
        public static readonly DependencyProperty LabelBrushProperty =
            DependencyProperty.RegisterAttached("LabelBrush", typeof(Brush), typeof(ExpanderHelper));

        public static void SetLabelBrush(Expander dependencyObject, Brush value)
        {
            dependencyObject.SetValue(LabelBrushProperty, value);
        }

        public static Brush GetLabelBrush(Expander dependencyObject)
        {
            return (Brush)dependencyObject.GetValue(LabelBrushProperty);
        }
    }
}
