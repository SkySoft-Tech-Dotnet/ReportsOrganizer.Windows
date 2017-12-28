using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace ReportsOrganizer.Core.Services
{
    public interface ITaskService
    {
        Task GetTask(string name);

        void UpdateTask(string name, TaskDefinition taskDefinition);
    }

    public class TaskService : ITaskService
    {
        public const string NotificationKey = "/notify";

        private TaskFolder _taskFolder;

        public TaskService()
        {
            using (var taskService = new Microsoft.Win32.TaskScheduler.TaskService())
            {
                _taskFolder = taskService.GetFolder("ReportsOrganizer")
                              ?? taskService.RootFolder.CreateFolder("ReportsOrganizer");
            }
            CheckPathes();
        }

        public Task GetTask(string name)
        {
            using (var taskService = new Microsoft.Win32.TaskScheduler.TaskService())
            {
                var task = taskService.FindTask(name);

                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportsOrganizer.exe");

                var action = new ExecAction(path, NotificationKey, null);

                if (task == null)
                {
                    var newTask = taskService.NewTask();
                    newTask.Actions.Add(action);
                    task = _taskFolder.RegisterTaskDefinition(name, newTask);
                }

                return task;
            }
        }

        public void UpdateTask(string name, TaskDefinition taskDefinition)
        {
            _taskFolder.RegisterTaskDefinition(name, taskDefinition);
        }

        private void CheckPathes()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportsOrganizer.exe");

            var action = new ExecAction(path, NotificationKey, null);

            foreach (var task in _taskFolder.AllTasks)
            {
                if (task.Definition.Actions.Count != 0 && ((ExecAction)task.Definition.Actions[0]).Path != path)
                {
                    var definition = task.Definition;
                    definition.Actions.Clear();
                    definition.Actions.Add(action);
                    UpdateTask(task.Name, definition);
                }
            }
        }
    }
}
