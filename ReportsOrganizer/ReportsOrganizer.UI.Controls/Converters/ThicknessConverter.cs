using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ReportsOrganizer.UI.Controls.Converters
{
    [Flags]
    public enum ThicknessType
    {
        Left = 0x01,
        Top = 0x02,
        Right = 0x04,
        Bottom = 0x08
    }

    internal sealed class ThicknessConverter : IValueConverter
    {
        public ThicknessType DisplayThickness { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness source && parameter is ThicknessType flags)
            {
                var thickness = new Thickness();
                if ((flags & ThicknessType.Left) != 0)
                {
                    thickness.Left = source.Left;
                }
                if ((flags & ThicknessType.Top) != 0)
                {
                    thickness.Top = source.Top;
                }
                if ((flags & ThicknessType.Right) != 0)
                {
                    thickness.Right = source.Right;
                }
                if ((flags & ThicknessType.Bottom) != 0)
                {
                    thickness.Bottom = source.Bottom;
                }
                return thickness;
            }
            return default(Thickness);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
