using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOrganizer.UI.Models
{
    public class ApplicationSettings
    {
        public GeneralSettings General { get; set; }
        public NotificationSettings Notification { get; set; }
    }

    public class GeneralSettings
    {
        public bool StartupWithWindows { get; set; }
        public bool StartMinimized { get; set; }
        public string Language { get; set; }
    }

    public class NotificationSettings
    {
        public int Type { get; set; }
        public int Interval { get; set; }
        public IEnumerable<int> Times { get; set; }
    }
}
