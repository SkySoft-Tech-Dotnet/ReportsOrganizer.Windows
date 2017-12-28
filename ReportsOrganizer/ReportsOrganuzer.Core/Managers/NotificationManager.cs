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

        bool Enabled { get; set; }

        void Notify();

        T GetService<T>() where T : IScheduleService;
    }

    public class NotificationManager : INotificationManager
    {
        private List<IScheduleService> _scheduleServices;
        
        public event EventHandler Notified;
        public bool Enabled { get; set; }

        public NotificationManager(
            IntervalScheduleService intervalScheduleService, 
            DailyScheduleService specificTimeScheduleService)
        {
            _scheduleServices = new List<IScheduleService>
            {
                intervalScheduleService,
                specificTimeScheduleService
            };

            foreach (var scheduleService in _scheduleServices)
            {
                scheduleService.ScheduleNotification += delegate { Notify(); };
            }
        }

        public T GetService<T>() where T : IScheduleService
        {
            return (T)_scheduleServices.FirstOrDefault(s => s is T);
        }
        
        public void Notify()
        {



            OnNotified();
        }

        public void Postpone()
        {
            
        }

        public void HandleSave()
        {
            
        }

        protected virtual void OnNotified()
        {
            Notified?.Invoke(this, EventArgs.Empty);
        }
    }
}
