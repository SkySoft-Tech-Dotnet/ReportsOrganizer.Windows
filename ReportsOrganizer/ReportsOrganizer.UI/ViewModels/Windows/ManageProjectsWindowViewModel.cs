using System.Threading;
using System.Windows.Input;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.Models;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Views.Windows;
using System.Threading.Tasks;

namespace ReportsOrganizer.UI.ViewModels.Windows
{
    public class ManageProjectsWindowViewModel : BaseViewModel
    {
        private IProjectService _projectService;

        public string ShortName { get; set; }
        public string FullName { get; set; }

        public ICommand OkCommand { get; }

        public ManageProjectsWindowViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            
            OkCommand = new AsyncCommand(OkAction);
        }

        private async Task OkAction(object obj)
        {
            await _projectService.AddAsync(new Project
            {
                ShortName = ShortName,
                FullName = FullName,
                IsActive = true
            }, CancellationToken.None);

            ((ManageProjectsWindowView) obj).Close();
        }
    }
}
