using ReportsOrganizer.UI.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.Models;
using ReportsOrganizer.UI.Command;

namespace ReportsOrganizer.UI.ViewModels.Windows
{
    public class NotificationWindowViewModel : BaseViewModel
    {
        private readonly IReportService _reportService;
        private IProjectService _projectService;

        private Visibility _windowVisibility;
        private string _description;
        private Project _selectedProject;
        private TimeSpan _selectedTime;

        public Visibility WindowVisibility
        {
            get => _windowVisibility;
            set
            {
                if (value == Visibility.Visible)
                {
                    LastReport = _reportService.GetLastReportAsync(CancellationToken.None).Result;
                    ProjectList = _projectService.ToListAsync(CancellationToken.None).Result
                        .OrderBy(property => property.ShortName);

                    NotifyPropertyChanged(nameof(ProjectList));
                    NotifyPropertyChanged(nameof(UsePreviousEnable));

                    SelectedProject = LastReport?.Project ?? ProjectList.FirstOrDefault();
                    Description = null;
                    SelectedTime = new TimeSpan();
                }
                SetValue(ref _windowVisibility, value, nameof(WindowVisibility));
            }
        }

        public string Description
        {
            get => _description;
            set => SetValue(ref _description, value, nameof(Description));
        }

        public Project SelectedProject
        {
            get => _selectedProject;
            set => SetValue(ref _selectedProject, value, nameof(SelectedProject));
        }

        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set => SetValue(ref _selectedTime, value, nameof(SelectedTime));
        }

        public bool UsePreviousEnable => LastReport != null;

        public Report LastReport { get; private set; }
        public IEnumerable<Project> ProjectList { get; private set; }
        public CultureInfo DurationCultureInfo { get; }

        public ICommand WindowClosingCommand { get; }
        public ICommand UsePreviousCommand { get; }
        public ICommand PostponeCommand { get; }
        public ICommand OkCommand { get; }

        public NotificationWindowViewModel(IReportService reportService, IProjectService projectService)
        {
            _reportService = reportService;
            _projectService = projectService;

            WindowClosingCommand = new RelayCommand(WindowClosingAction, true);
            UsePreviousCommand = new RelayCommand(UsePreviousAction, true);
            PostponeCommand = new RelayCommand(PostponeAction, true);
            OkCommand = new RelayCommand(OkAction, true);

            DurationCultureInfo = new CultureInfo("uk-UA")
            {
                DateTimeFormat =
                {
                    ShortTimePattern = "HH:mm",
                    LongTimePattern = "HH:mm"
                }
            };
        }

        private void WindowClosingAction(object sender)
        {
            ((CancelEventArgs)sender).Cancel = true;
            WindowVisibility = Visibility.Hidden;
        }

        private void UsePreviousAction(object obj)
        {
            SelectedProject = LastReport.Project;
            Description = LastReport.Description;
        }

        private void PostponeAction(object obj)
        {
            WindowVisibility = Visibility.Hidden;
        }

        private void OkAction(object obj)
        {
            _reportService.AddAsync(new Report
            {
                Description = _description,
                Created = DateTime.Now,
                Duration = (int)_selectedTime.TotalMinutes,
                Project = _selectedProject
            }, CancellationToken.None);

            WindowVisibility = Visibility.Hidden;
        }
    }
}
