using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.Models;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.ViewModels.Windows;
using ReportsOrganizer.UI.Views.Windows;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class ManageProjectsViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;

        public BindingList<Project> ProjectList { get; set; }

        public ICommand CreateProjectCommand { get; }
        //public ICommand WindowClosingCommand { get; }

        public ManageProjectsViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            Task.Run(async () =>
            {
                var result = await projectService.ToListAsync(CancellationToken.None);
                ProjectList = new BindingList<Project>(result.ToList());
            }).Wait();

            CreateProjectCommand = new RelayCommand(CreateProjectAction, true);
        }

        private void CreateProjectAction(object obj)
        {
            var window = new ManageProjectsWindowView();
            window.Closing += WindowClosingAction;
            window.Show();
        }

        private void WindowClosingAction(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                var result = await _projectService.ToListAsync(CancellationToken.None);
                ProjectList = new BindingList<Project>(result.ToList());
            }).Wait();
            NotifyPropertyChanged(nameof(ProjectList));
        }
    }
}
