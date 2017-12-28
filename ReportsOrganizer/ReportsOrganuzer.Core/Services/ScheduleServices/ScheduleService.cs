using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Services.ScheduleServices
{
    public interface IScheduleService
    {
        event EventHandler ScheduleNotification;
    }

    public abstract class ScheduleServiceBase : IScheduleService
    {
        public event EventHandler ScheduleNotification;

        protected virtual void OnScheduleNotification()
        {
            ScheduleNotification?.Invoke(this, EventArgs.Empty);
        }
    }
}
