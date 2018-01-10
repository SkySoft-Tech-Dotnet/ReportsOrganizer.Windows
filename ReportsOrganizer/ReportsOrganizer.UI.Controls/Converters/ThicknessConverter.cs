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

    public enum ThicknessTypeAngle
    {
        TopRight = ThicknessType.Top | ThicknessType.Right,
        RightBottom = ThicknessType.Right | ThicknessType.Bottom,
        BottomLeft = ThicknessType.Bottom | ThicknessType.Left,
        LeftTop = ThicknessType.Left | ThicknessType.Top
    }

    internal sealed class ThicknessConverter : IValueConverter
    {
        public ThicknessType DisplayThickness { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Thickness source))
            {
                return default(Thickness);
            }

            var thickness = new Thickness();
            if (parameter is ThicknessTypeAngle flag)
            {
                switch (flag)
                {
                    case ThicknessTypeAngle.TopRight:
                        thickness.Top = source.Top;
                        thickness.Right = source.Right;
                        break;
                    case ThicknessTypeAngle.RightBottom:
                        thickness.Top = source.Right;
                        thickness.Right = source.Bottom;
                        break;
                    case ThicknessTypeAngle.BottomLeft:
                        thickness.Top = source.Bottom;
                        thickness.Right = source.Left;
                        break;
                    case ThicknessTypeAngle.LeftTop:
                        thickness.Top = source.Left;
                        thickness.Right = source.Top;
                        break;
                }
            }
            else if (parameter is ThicknessType flags)
            {
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
            }
            return thickness;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
