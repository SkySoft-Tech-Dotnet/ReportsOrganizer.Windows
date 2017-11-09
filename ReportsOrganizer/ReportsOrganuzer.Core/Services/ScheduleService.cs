using ReportsOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ReportsOrganizer.Core.Services
{
    public interface IScheduleService
    {
        void SetSchedule(int time);
    }

    public class ScheduleService : IScheduleService
    {

        //private ScheduleTypes _type;
        //private int _interval;
        //private IEnumerable<int> _times;
        

        public ScheduleService(IConfigurationService<ApplicationSettings> notificationService)
        {
            //_notificationService = notificationService;

            //_type = _notificationService.Value.Notification.Type;
            //_interval = _notificationService.Value.Notification.Interval;
            //_times = _notificationService.Value.Notification.Times;
            
        }



        public void SetSchedule(int time)
        {
            Timer checkForTime = new Timer(time)
            {
                //Elapsed += new ElapsedEventHandler((sender, args) => ),
                Enabled = true
            };
        }
    }
}
