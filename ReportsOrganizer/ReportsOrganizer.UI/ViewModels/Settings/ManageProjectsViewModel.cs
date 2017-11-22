using System;
using System.ComponentModel;
using System.Threading;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.Models;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class ManageProjectsViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;

        public ManageProjectsViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            CurrentProjects = new BindingList<Project>();
            CurrentProjects.Add(new Project
            {
                ShortName = "UCCC",
                FullName = "Universal",
                IsActive = true
            });
            CurrentProjects.Add(new Project
            {
                ShortName = "RO",
                FullName = "ReportOrganaizer",
                IsActive = true
            });
            CurrentProjects.Add(new Project
            {
                ShortName = "BCH",
                FullName = "Blockchain",
                IsActive = true
            });
        }

        public BindingList<Project> CurrentProjects { get; set; }

    }
}
