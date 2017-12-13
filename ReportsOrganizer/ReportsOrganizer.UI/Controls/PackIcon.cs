using System;
using System.Collections.Generic;
using System.Windows;

namespace ReportsOrganizer.UI.Controls
{
    public enum PackIconFlipOrientation
    {
        Normal, Horizontal, Vertical, Both
    }

    public class PackIcon<TKind> : PackIconBase<TKind>
    {
        public static readonly DependencyProperty FlipProperty
            = DependencyProperty.Register("Flip", typeof(PackIconFlipOrientation), typeof(PackIcon<TKind>),
                new PropertyMetadata(PackIconFlipOrientation.Normal));

        public static readonly DependencyProperty RotationProperty
            = DependencyProperty.Register("Rotation", typeof(double), typeof(PackIcon<TKind>),
                new PropertyMetadata(0d, null, RotationPropertyCoerceValueCallback));

        private static object RotationPropertyCoerceValueCallback(DependencyObject dependencyObject, object value)
        {
            var val = (double)value;
            return val < 0 ? 0d : (val > 360 ? 360d : value);
        }

        public PackIconFlipOrientation Flip
        {
            get => (PackIconFlipOrientation)GetValue(FlipProperty);
            set => SetValue(FlipProperty, value);
        }

        public double Rotation
        {
            get => (double)GetValue(RotationProperty);
            set => SetValue(RotationProperty, value);
        }
        
        public PackIcon(Func<IDictionary<TKind, string>> dataIndexFactory) : base(dataIndexFactory)
        {
        }
    }
}
