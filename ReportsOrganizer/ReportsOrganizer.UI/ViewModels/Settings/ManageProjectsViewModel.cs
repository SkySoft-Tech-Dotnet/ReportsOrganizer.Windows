﻿using System;
using System.ComponentModel;
using System.Threading;
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
            CurrentProjects = new BindingList<Project>();
            CurrentProjects.Add(new Project
            {
                ShortName = "UCCC",
                FullName = "Universal",
                Active = true
            });
            CurrentProjects.Add(new Project
            {
                ShortName = "RO",
                FullName = "ReportOrganaizer",
                Active = true
            });
            CurrentProjects.Add(new Project
            {
                ShortName = "BCH",
                FullName = "Blockchain",
                Active = true
            });
        }

        public BindingList<Project> CurrentProjects { get; set; }

    }
}
