using System;

namespace ReportsOrganizer.UI.Controls.ViewModel
{
    internal sealed class ReportGroupViewModel
    {
        public DateTime Date { get; set; }

        public int Count { get; set; }
        public string Projects { get; set; }
        public int Time { get; set; }
    }
}
