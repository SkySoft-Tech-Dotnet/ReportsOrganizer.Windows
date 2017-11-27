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

        public int Id { get; set; }

        private string _shortName;
        private string _fullName;

        [UniqueShortName(nameof(Id))]
        [Required(AllowEmptyStrings = false)]
        public string ShortName
        {
            get => _shortName;
            set
            {
                SetValue(ref _shortName, value, nameof(ShortName));
                ValidatePropertyAsync(nameof(ShortName), value);
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string FullName
        {
            get => _fullName;
            set => SetValue(ref _fullName, value, nameof(FullName));
        }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }


        public ManageProjectsWindowViewModel(IProjectService projectService)
        {
            _projectService = projectService;

            OkCommand = new AsyncCommand(OkAction);
            CancelCommand = new RelayCommand(CancelAction, true);
        }


        private async Task OkAction(object obj)
        {
            Validate();
            if (HasErrors)
                return;

            await _projectService.AddOrUpdateAsync(new Project
            {
                Id = Id,
                ShortName = ShortName,
                FullName = FullName
            }, CancellationToken.None);

            ((ManageProjectsWindowView)obj).DialogResult = true;
            ((ManageProjectsWindowView)obj).Close();
        }

        private void CancelAction(object obj)
        {
            ((ManageProjectsWindowView)obj).Close();
        }
    }
}
