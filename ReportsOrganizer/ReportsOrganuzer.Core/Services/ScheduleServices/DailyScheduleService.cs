using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;

namespace ReportsOrganizer.Core.Services.ScheduleServices
{
    public class DailyScheduleService : ScheduleServiceBase
    {
        private const string TaskName = "DailyTask";

        private ITaskService _taskService;

        public DailyScheduleService(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void AddTask(TimeSpan start)
        {
            var task = _taskService.GetTask(TaskName);
            var deffinition = task.Definition;
            deffinition.Triggers.Add(new DailyTrigger
            {
                StartBoundary = DateTime.Today.Add(start)
            });
            _taskService.UpdateTask(TaskName, deffinition);
        }

        public Dictionary<string, TimeSpan> GetTasks()
        {
            var task = _taskService.GetTask(TaskName);
            var definition = task.Definition;
            var dictionary = new Dictionary<string, TimeSpan>();
            foreach (var trigger in definition.Triggers)
                dictionary.Add(trigger.Id, trigger.StartBoundary.TimeOfDay);
            return dictionary;
        }
    }
}
