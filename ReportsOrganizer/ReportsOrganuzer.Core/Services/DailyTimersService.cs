using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ReportsOrganizer.Core.Services
{
    public interface IDailyTimersService
    {
        Action Action { get; set; }
        bool IsEnabled { get; set; }
        void SetTimes(IEnumerable<int> times);
    }

    internal class DailyTimersesService : IDailyTimersService
    {
        private List<DispatcherTimer> _timerList;

        private Action _action;
        private bool _isEnabled;

        public Action Action { get; set; }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _timerList.ForEach(e => e.IsEnabled = value);
                _isEnabled = value;
            }
        }

        public DailyTimersesService()
        {
            _timerList = new List<DispatcherTimer>();
        }

        public void SetTimes(IEnumerable<int> times)
        {
            _timerList.Clear();
            foreach (var e in times)
            {
                var tmp = new DispatcherTimer
                {
                    Interval = SetTime(e)
                };
                tmp.Tick += InvokeAction;
                tmp.Tick += RefreshTimer;
                _timerList.Add(tmp);
            }
        }

        private void RefreshTimer(object sender, EventArgs e)
        {
            ((DispatcherTimer) sender).Interval = TimeSpan.FromDays(1);
        }

        private void InvokeAction(object sender, EventArgs e)
        {
            Action();
        }

        private TimeSpan SetTime(int time)
        {
            DateTime nowTime = DateTime.Now;
            DateTime scheduledTime = DateTime.Today.AddMinutes(time);
            //DateTime scheduledTime = DateTime.Now.AddSeconds(time);
            if (nowTime > scheduledTime)
                scheduledTime = scheduledTime.AddDays(1);
            return scheduledTime - DateTime.Now;
        }
    }
}
