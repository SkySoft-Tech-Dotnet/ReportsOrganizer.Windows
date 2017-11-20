using ReportsOrganizer.Core.Enums;
using ReportsOrganizer.Core.Managers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ReportsOrganizer.Core.Services
{
    public class JobService
    {
        private bool _launched = false;
        private DispatcherTimer _timer;
        private object _sync = new object();

        public string Id { get; set; }
        public bool Enabled { get; set; }

        public JobScheduleType Type { get; private set; }
        public DateTime NextRun { get; private set; }
        public TimeSpan? Init { get; }
        public IEnumerable<Func<Task>> Tasks { get; }
        public Func<DateTime> CalculateNextRun { get; private set; }

        public JobService()
        {
            Id = Guid.NewGuid().ToString();
            Enabled = true;

            Tasks = new List<Func<Task>>();
        }

        public JobService(Func<Task> task) : this()
        {
            AddTask(task);
        }

        public JobService AddTask(Func<Task> task)
        {
            (Tasks as ICollection<Func<Task>>).Add(task);
            return this;
        }

        public JobService ToRunAt(TimeSpan timeSpan)
        {
            ToRunOnceAt(timeSpan);
            CalculateNextRun = () =>
            {
                return NextRun.AddDays(1);
            };
            Type = JobScheduleType.SpecificTime;
            return this;
        }

        public JobService ToRunOnceAt(TimeSpan timeSpan)
        {
            ThrowIfLaunched();

            NextRun = DateTime.Today + timeSpan;
            Type = JobScheduleType.SpecificTime;

            return this;
        }

        public JobService ToRunEvery(TimeSpan timeSpan)
        {
            ThrowIfLaunched();

            NextRun = Init == null
                ? DateTime.Now + timeSpan
                : CalculateOffsetNextRun(timeSpan);

            CalculateNextRun = () =>
            {
                return NextRun + timeSpan;
            };
            Type = JobScheduleType.Repeat;

            return this;
        }

        internal void Run(SchedulerManage manage)
        {
            _launched = true;
            _timer = new DispatcherTimer
            {
                Interval = NextRun - DateTime.Now
            };

            _timer.Tick += (sender, e) =>
            {
                lock (_sync)
                {
                    _timer.Stop();
                    if (manage.CanExecute(this) && Enabled)
                    {
                        (Tasks as List<Func<Task>>).ForEach(task => task());
                    }
                    if (CalculateNextRun != null)
                    {
                        NextRun = CalculateNextRun();
                        _timer.Interval = NextRun - DateTime.Now;
                        _timer.Start();
                    }
                }
            };

            _timer.Start();
        }

        internal void Disable()
        {
            lock (_sync)
            {
                _timer.IsEnabled = false;
                _timer.Stop();
            }
        }

        private DateTime CalculateOffsetNextRun(TimeSpan timeSpan)
        {
            var diff = DateTime.Now.Subtract(DateTime.Today.Add(Init.Value));
            var iterators = (int)(diff.TotalMinutes / timeSpan.TotalMinutes);

            if (diff.TotalMinutes % timeSpan.TotalMinutes > 0)
            {
                iterators++;
            }

            return DateTime.Today
                .Add(Init.Value)
                .Add(TimeSpan.FromMinutes(iterators * timeSpan.TotalMinutes));
        }

        private void ThrowIfLaunched()
        {
            if (_launched)
            {
                throw new Exception("Job already started!");
            }
        }
    }
}
