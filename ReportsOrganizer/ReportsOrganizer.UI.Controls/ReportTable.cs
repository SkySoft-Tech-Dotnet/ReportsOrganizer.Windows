using ReportsOrganizer.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ReportsOrganizer.UI.Controls
{
    public class ReportTable : Control
    {
        public IEnumerable<Report> Source
        {
            get => (IEnumerable<Report>)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        static ReportTable()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ReportTable),
                new FrameworkPropertyMetadata(typeof(ReportTable)));
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(IEnumerable<Report>), typeof(ReportTable));
    }
}
