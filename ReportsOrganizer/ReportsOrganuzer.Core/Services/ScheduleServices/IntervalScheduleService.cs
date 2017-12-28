using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;

namespace ReportsOrganizer.Core.Services.ScheduleServices
{
    public class IntervalScheduleService : ScheduleServiceBase
    {
        private const string TaskName = "IntervalTask";
        private ITaskService _taskService;

        public IntervalScheduleService(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void AddInterval(TimeSpan interval)
        {
            if(interval == TimeSpan.Zero)
                return;
            if (interval < TimeSpan.FromMinutes(30))
                interval = TimeSpan.FromMinutes(30);

            var task = _taskService.GetTask(TaskName);
            var definition = task.Definition;
            definition.Triggers.Clear();

            for (var t = TimeSpan.Zero; t < TimeSpan.FromDays(1); t += interval)
            {
                definition.Triggers.Add(new DailyTrigger
                {
                    StartBoundary = DateTime.Today.Add(t)
                });
            }

            _taskService.UpdateTask(TaskName, definition);
        }

        public TimeSpan GetPeriod()
        {
            var task = _taskService.GetTask(TaskName);
            var definition = task.Definition;
            if(definition.Triggers.Count == 0)
                return TimeSpan.Zero;
            return definition.Triggers[1].StartBoundary.TimeOfDay - definition.Triggers[0].StartBoundary.TimeOfDay;
        }
    }
}
