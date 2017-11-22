using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.Models;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class ManageProjectsViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;

        public ICommand CreateProjectCommand;

        public ManageProjectsViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            Task.Run(async () =>
            {
                var result = await projectService.ToListAsync(CancellationToken.None);
                CurrentProjects = new BindingList<Project>(result.ToList());
            }).Wait();
            
        }

        public BindingList<Project> CurrentProjects { get; set; }

    }
}
