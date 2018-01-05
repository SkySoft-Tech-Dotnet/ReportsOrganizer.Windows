using ReportsOrganizer.UI.Controls.Abstractions;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ReportsOrganizer.UI.Controls
{
    public enum IconFlipOrientation
    {
        Normal, Horizontal, Vertical, Both
    }

    public class Icon<TKind> : IconBase<TKind>
    {
        public static readonly DependencyProperty FlipProperty
            = DependencyProperty.Register("Flip", typeof(IconFlipOrientation), typeof(Icon<TKind>),
                new PropertyMetadata(IconFlipOrientation.Normal));

        public static readonly DependencyProperty RotationProperty
            = DependencyProperty.Register("Rotation", typeof(double), typeof(Icon<TKind>),
                new PropertyMetadata(0d, null, RotationPropertyCoerceValueCallback));

        private static object RotationPropertyCoerceValueCallback(DependencyObject dependencyObject, object value)
        {
            var val = (double)value;
            return val < 0 ? 0d : (val > 360 ? 360d : value);
        }

        public IconFlipOrientation Flip
        {
            get => (IconFlipOrientation)GetValue(FlipProperty);
            set => SetValue(FlipProperty, value);
        }

        public double Rotation
        {
            get => (double)GetValue(RotationProperty);
            set => SetValue(RotationProperty, value);
        }

        public Icon(Func<IDictionary<TKind, string>> dataIndexFactory)
            : base(dataIndexFactory)
        {
        }
    }
}
