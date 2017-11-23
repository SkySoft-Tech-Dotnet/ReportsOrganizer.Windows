using System;
using System.Collections.Generic;
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
using System.Windows;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class ManageProjectsViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;

        public IEnumerable<Project> ProjectList => _projectService.ToListAsync(CancellationToken.None).Result;

        public ICommand CreateProjectCommand { get; }
        public ICommand DeleteProjectCommand { get; }
        public ICommand ChangeProjectIsActiveCommand { get; }
        //public ICommand WindowClosingCommand { get; }

        public ManageProjectsViewModel(IProjectService projectService, ViewModelLocator viewModelLocator)
        {
            _projectService = projectService;

            CreateProjectCommand = new RelayCommand(CreateProjectAction, true);
            DeleteProjectCommand = new AsyncCommand(DeleteProjectAction);
            ChangeProjectIsActiveCommand = new AsyncCommand(ChangeProjectIsActiveAction);
        }

        private void CreateProjectAction(object obj)
        {
            var window = new ManageProjectsWindowView();
            var context = window.DataContext as ManageProjectsWindowViewModel;
            
            window.Closing += (sender, args) => NotifyPropertyChanged(nameof(ProjectList));

            window.ShowDialog();
        }

        private async Task DeleteProjectAction(object obj)
        {
            await _projectService.DeleteAsync(obj as Project, CancellationToken.None);
            NotifyPropertyChanged(nameof(ProjectList));
        }

        private async Task ChangeProjectIsActiveAction(object obj)
        {
            await _projectService.SaveChangesAsync(CancellationToken.None);
            NotifyPropertyChanged(nameof(ProjectList));
        }
    }
}
