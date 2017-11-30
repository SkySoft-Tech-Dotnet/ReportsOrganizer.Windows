using ReportsOrganizer.Core.Services;
using ReportsOrganizer.Models;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.ViewModels.Windows;
using ReportsOrganizer.UI.Views.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class ManageProjectsViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;

        public IEnumerable<Project> ProjectList =>
            _projectService.ToListAsync(CancellationToken.None).Result;

        public IEnumerable<Project> ActiveProjectList =>
            ProjectList.Where(p => p.IsActive).OrderBy(p => p.ShortName);

        public IEnumerable<Project> InactiveProjectList =>
            ProjectList.Where(p => p.IsActive == false).OrderBy(p => p.ShortName);



        public ICommand CreateProjectCommand { get; }
        public ICommand EditProjectCommand { get; }
        public ICommand IsActiveProjectCommand { get; }
        public ICommand DeleteProjectCommand { get; }

        public ManageProjectsViewModel(IProjectService projectService)
        {
            _projectService = projectService;

            CreateProjectCommand = new RelayCommand(CreateProjectAction, true);
            EditProjectCommand = new RelayCommand(EditProjectAction);
            IsActiveProjectCommand = new AsyncCommand(IsActiveProjectAction);
            DeleteProjectCommand = new AsyncCommand(DeleteProjectAction);
        }

        private void CreateProjectAction(object obj)
        {
            var window = new ManageProjectsWindowView();
            window.Owner = Application.Current.MainWindow;
            if (window.ShowDialog() == true)
                NotifyPropertyChanged(nameof(ActiveProjectList));
        }

        private void EditProjectAction(object obj)
        {
            var window = new ManageProjectsWindowView();
            window.Owner = Application.Current.MainWindow;
            var context = (ManageProjectsWindowViewModel)window.DataContext;
            var project = (Project)obj;

            context.CurrentProject = project;

            if (window.ShowDialog() == true)
            {
                NotifyPropertyChanged(nameof(ActiveProjectList));
                NotifyPropertyChanged(nameof(InactiveProjectList));
            }
        }

        private async Task IsActiveProjectAction(object obj)
        {
            await _projectService.SaveChangesAsync(CancellationToken.None);
            NotifyPropertyChanged(nameof(ActiveProjectList));
            NotifyPropertyChanged(nameof(InactiveProjectList));
        }

        private async Task DeleteProjectAction(object obj)
        {
            var result = MessageBox.Show("Do you want to delete this element?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                await _projectService.DeleteAsync(obj as Project, CancellationToken.None);
                NotifyPropertyChanged(nameof(ActiveProjectList));
                NotifyPropertyChanged(nameof(InactiveProjectList));
            }
        }
    }
}
