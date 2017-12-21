using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsOrganizer.Core.Services.ScheduleServices;

namespace ReportsOrganizer.Core.Managers
{
    public class TimeRange
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public interface INotificationManager
    {
        event EventHandler Notified;

        void Notify();
    }

    public class NotificationManager : INotificationManager
    {
        private ScheduleService _intervalScheduleService;
        private ScheduleService _specificTimeScheduleService;

        private IEnumerable<TimeRange> _ignoreTimes;
        private Dictionary<string, TimeSpan> _intervals;
        private Dictionary<string, TimeSpan> _specificTimes;

        public event EventHandler Notified;

        public NotificationManager(IntervalScheduleService intervalScheduleService, SpecificTimeScheduleService specificTimeScheduleService)
        {
            _intervalScheduleService = intervalScheduleService;
            _specificTimeScheduleService = specificTimeScheduleService;

            _intervals = new Dictionary<string, TimeSpan>();
            _specificTimes = new Dictionary<string, TimeSpan>();
            _ignoreTimes = new List<TimeRange>();
        }

        public void AddInterval()
        {
            
        }


        public void Notify()
        {
            OnNotified();
        }

        protected virtual void OnNotified()
        {
            Notified?.Invoke(this, EventArgs.Empty);
        }
    }
}
