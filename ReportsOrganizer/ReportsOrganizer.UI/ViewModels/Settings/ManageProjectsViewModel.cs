using ReportsOrganizer.Core.Services;
using ReportsOrganizer.Models;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Managers;
using ReportsOrganizer.UI.ViewModels.Windows;
using ReportsOrganizer.UI.Views.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class ManageProjectsViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;
        private readonly ApplicationManager _applicationManager;
        private IEnumerable<Project> _projectList;

        public IEnumerable<Project> ActiveProjectList =>
            _projectList.Where(p => p.IsActive).OrderBy(p => p.ShortName);

        public IEnumerable<Project> InactiveProjectList =>
            _projectList.Where(p => p.IsActive == false).OrderBy(p => p.ShortName);

        public ICommand CreateProjectCommand { get; }
        public ICommand EditProjectCommand { get; }
        public ICommand IsActiveProjectCommand { get; }
        public ICommand DeleteProjectCommand { get; }

        public ManageProjectsViewModel(IProjectService projectService, ApplicationManager applicationManager)
        {
            _projectService = projectService;
            _applicationManager = applicationManager;

            CreateProjectCommand = new AsyncCommand(CreateProjectAction);
            EditProjectCommand = new RelayCommand(EditProjectAction);
            IsActiveProjectCommand = new AsyncCommand(IsActiveProjectAction);
            DeleteProjectCommand = new AsyncCommand(DeleteProjectAction);

            Task.Run(async () => await LoadProjects()).Wait();
        }

        private async Task CreateProjectAction(object obj)
        {
            var window = new ManageProjectsWindowView { Owner = Application.Current.MainWindow };
            if (window.ShowDialog() == true)
            {
                await LoadProjects();
                ProjectsUpdated();
            }
        }

        private void EditProjectAction(object obj)
        {
            var window = new ManageProjectsWindowView { Owner = Application.Current.MainWindow };
            var context = (ManageProjectsWindowViewModel)window.DataContext;
            var project = (Project)obj;

            context.CurrentProject = project;

            if (window.ShowDialog() == true)
            {
                ProjectsUpdated();
            }
        }

        private async Task IsActiveProjectAction(object obj)
        {
            await _projectService.SaveChangesAsync(CancellationToken.None);
            ProjectsUpdated();
        }

        private async Task DeleteProjectAction(object obj)
        {
            var result = MessageBox.Show("Do you want to delete this element?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                await _projectService.DeleteAsync(obj as Project, CancellationToken.None);
                await LoadProjects();
                ProjectsUpdated();
            }
        }

        private void ProjectsUpdated()
        {
            NotifyPropertyChanged(nameof(ActiveProjectList));
            NotifyPropertyChanged(nameof(InactiveProjectList));
        }

        private async Task LoadProjects() 
            => _projectList = await _projectService.ToListAsync(CancellationToken.None);
    }
}
