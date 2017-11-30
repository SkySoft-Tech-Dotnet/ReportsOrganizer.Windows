using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Windows.Input;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.Models;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Views.Windows;
using System.Threading.Tasks;
using System.Windows;
using ReportsOrganizer.UI.Attributes;

namespace ReportsOrganizer.UI.ViewModels.Windows
{
    public class ManageProjectsWindowViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;

        private Project _currentProject;
        public Project CurrentProject
        {
            get => _currentProject;
            set
            {
                _currentProject = value;
                NotifyPropertyChanged(nameof(ShortName));
                NotifyPropertyChanged(nameof(FullName));
            }
        }
        

        [UniqueShortName(nameof(CurrentProject))]
        [Required(AllowEmptyStrings = false)]
        public string ShortName
        {
            get => CurrentProject.ShortName;
            set
            {
                CurrentProject.ShortName = value;
                NotifyPropertyChanged(nameof(ShortName));
                ClearValidationErrors(nameof(ShortName));
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string FullName
        {
            get => CurrentProject.FullName;
            set
            {
                CurrentProject.FullName = value;
                NotifyPropertyChanged(nameof(FullName));
                ClearValidationErrors(nameof(FullName));
            }
        }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }


        public ManageProjectsWindowViewModel(IProjectService projectService)
        {
            _projectService = projectService;

            OkCommand = new AsyncCommand(OkAction);
            CancelCommand = new RelayCommand(CancelAction, true);

            CurrentProject = new Project();
        }


        private async Task OkAction(object obj)
        {
            Validate();
            if (HasErrors)
                return;

            await _projectService.AddOrUpdateAsync(CurrentProject, CancellationToken.None);

            ((ManageProjectsWindowView)obj).DialogResult = true;
            ((ManageProjectsWindowView)obj).Close();
        }

        private void CancelAction(object obj)
        {
            ((ManageProjectsWindowView)obj).Close();
        }
    }
}
