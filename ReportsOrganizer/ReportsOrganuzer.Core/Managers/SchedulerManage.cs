using ReportsOrganizer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportsOrganizer.Core.Managers
{
    public interface ISchedulerManage
    {
        bool EnabledIgnoreSpecificTime { get; set; }
        bool EnabledRepeatJobs { get; set; }
        bool EnabledJobsWithSpecificTime { get; set; }

        IEnumerable<JobService> Jobs { get; }
        ISchedulerManage AddJob(JobService service);
        ISchedulerManage AddIgnore(TimeSpan begin, TimeSpan end);

        void RemoveJob(string id);
        void RemoveIgnore(string id);
    }

    internal class SchedulerManage : ISchedulerManage
    {
        public bool EnabledIgnoreSpecificTime { get; set; }
        public bool EnabledRepeatJobs { get; set; }
        public bool EnabledJobsWithSpecificTime { get; set; }

        public IEnumerable<JobService> Jobs { get; }
        public IDictionary<string, Func<bool>> CalculateIgnore { get; }

        public SchedulerManage()
        {
            Jobs = new List<JobService>();
            CalculateIgnore = new Dictionary<string, Func<bool>>();
        }

        public ISchedulerManage AddJob(JobService service)
        {
            service.Run(this);
            (Jobs as ICollection<JobService>).Add(service);
            return this;
        }

        public ISchedulerManage AddIgnore(TimeSpan begin, TimeSpan end)
        {
            (CalculateIgnore as Dictionary<string, Func<bool>>).Add(Guid.NewGuid().ToString(), () =>
            {
                return DateTime.Now.TimeOfDay >= begin && DateTime.Now.TimeOfDay <= end;
            });
            return this;
        }

        public void RemoveJob(string id)
        {
            var job = Jobs.First(service => service.Id == id);
            (Jobs as ICollection<JobService>).Remove(job);
            job.Disable();
        }

        public void RemoveIgnore(string id)
        {
            (CalculateIgnore as Dictionary<string, Func<bool>>).Remove(id);
        }

        public bool CanExecute(JobService service)
        {
            if (EnabledIgnoreSpecificTime && CalculateIgnore.Any(ignore => ignore.Value()))
            {
                return false;
            }
            if (!EnabledRepeatJobs && service.Type == Enums.JobScheduleType.Repeat)
            {
                return false;
            }
            if (!EnabledJobsWithSpecificTime && service.Type == Enums.JobScheduleType.SpecificTime)
            {
                return false;
            }
            return true;
        }
    }
}
