using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ReportsOrganizer.Core.Services
{
    public interface IFixedTimerService
    {
        Action Action { set; }
        bool IsEnabled { get; set; }
        void SetInterval(int minutes);
    }

    internal class FixedTimerService : IFixedTimerService
    {
        private readonly DispatcherTimer _intervalTimer;

        public FixedTimerService()
        {
            _intervalTimer = new DispatcherTimer();
        }

        public Action Action
        {
            //get => _intervalTimer.Tick;
            set
            {
                _intervalTimer.Tick += delegate { value(); };
            }
        }

        public bool IsEnabled
        {
            get => _intervalTimer.IsEnabled;
            set => _intervalTimer.IsEnabled = value;
        }

        public void SetInterval(int minutes)
        {
            _intervalTimer.Interval = TimeSpan.FromMinutes(minutes);
        }
    }
}
