using ReportsOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ReportsOrganizer.Core.Services
{
    public interface IScheduleService
    {
        bool IsIntervalEnabled { get; set; }
        bool IsTimesEnabled { get; set; }
        Action Action { set; }
        void RefreshFixedTimer();
        void RefreshDailyTimers();
    }

    public class ScheduleService : IScheduleService
    {
        private readonly IConfigurationService<ApplicationSettings> _configurationService;
        private readonly IFixedTimerService _fixedTimerService;
        private readonly IDailyTimersService _dailyTimersService;

        private Action _action;

        public Action Action
        {
            get => _action;
            set
            {
                _fixedTimerService.Action = value;
                _dailyTimersService.Action = value;
                _action = value;
            }
        }

        public bool IsIntervalEnabled
        {
            get => _fixedTimerService.IsEnabled;
            set => _fixedTimerService.IsEnabled = value;
        }

        public bool IsTimesEnabled
        {
            get => _dailyTimersService.IsEnabled;
            set => _dailyTimersService.IsEnabled = value;
        }


        public ScheduleService(
            IConfigurationService<ApplicationSettings> configurationService,
            IFixedTimerService fixedTimerService, 
            IDailyTimersService dailyTimersService)
        {
            _configurationService = configurationService;
            _fixedTimerService = fixedTimerService;
            _dailyTimersService = dailyTimersService;
            
            Initialize();
        }
        
        private void Initialize()
        {
            RefreshFixedTimer();
            RefreshDailyTimers();
        }

        public void RefreshFixedTimer()
        {
            _fixedTimerService.SetInterval(_configurationService.Value.Notification.Interval);
            _fixedTimerService.IsEnabled = _configurationService.Value.Notification.IntervalEnabled;
        }

        public void RefreshDailyTimers()
        {
            _dailyTimersService.SetTimes(_configurationService.Value.Notification.Times);
            _dailyTimersService.IsEnabled = _configurationService.Value.Notification.TimesEnabled;
        }
    }
}
