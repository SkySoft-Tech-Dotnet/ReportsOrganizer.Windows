using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;

namespace ReportsOrganizer.Core.Services
{
    public interface ITaskService
    {
        void AddTask(DateTime start, TimeSpan period);
    }

    public class TaskService : ITaskService
    {
        public void AddTask(DateTime start, TimeSpan period)
        {
            using (var ts = new Microsoft.Win32.TaskScheduler.TaskService())
            {
                var task = ts.NewTask();

                var folder = ts.GetFolder("ReportOrganizer")
                             ?? ts.RootFolder.CreateFolder("ReportOrganizer");

                task.RegistrationInfo.Description = "My first task scheduler";

                task.Triggers.Add(new TimeTrigger()
                {
                    StartBoundary = new DateTime(2017, 12, 18, 17, 20, 0), 
                    //Repetition = new RepetitionPattern()
                });

                task.Actions.Add(new ExecAction(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportOrganizer.exe"), null, null));

                //task.Settings.DeleteExpiredTaskAfter = new TimeSpan(30,0,0,0);

                folder.RegisterTaskDefinition("TaskName", task);
            }
        }
    }
}
