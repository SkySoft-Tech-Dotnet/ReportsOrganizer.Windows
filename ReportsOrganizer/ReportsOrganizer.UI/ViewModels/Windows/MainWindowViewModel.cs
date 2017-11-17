using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Helpers;
using ReportsOrganizer.UI.ViewModels.Settings;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ReportsOrganizer.UI.Services;

namespace ReportsOrganizer.UI.ViewModels.Windows
{
    public class MainWindowViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        private bool _settingsIsOpen;
        private string _headerSettingsGroup;

        private Visibility _windowVisibility;
        private Visibility _settingsGroupVisibility;
        private Visibility _navigationSettingsVisibility;

        private WindowState _prevWindowState;
        private WindowState _currentWindowState;

        private BaseViewModel _currentSettingsGroup;

        public Visibility WindowVisibility
        {
            get => _windowVisibility;
            set => SetValue(ref _windowVisibility, value, nameof(WindowVisibility));
        }

        public WindowState CurrentWindowState
        {
            get => _currentWindowState;
            set
            {
                _prevWindowState = _currentWindowState;
                SetValue(ref _currentWindowState, value, nameof(CurrentWindowState));
            }
        }

        public bool SettingsIsOpen
        {
            get => _settingsIsOpen;
            set => SetValue(ref _settingsIsOpen, value, nameof(SettingsIsOpen));
        }

        public BaseViewModel CurrentSettingsGroup
        {
            get => _currentSettingsGroup;
            set => SetValue(ref _currentSettingsGroup, value, nameof(CurrentSettingsGroup));
        }

        public Visibility SettingsGroupVisibility
        {
            get => _settingsGroupVisibility;
            set => SetValue(ref _settingsGroupVisibility, value, nameof(SettingsGroupVisibility));
        }

        public Visibility NavigationSettingsVisibility
        {
            get => _navigationSettingsVisibility;
            set => SetValue(ref _navigationSettingsVisibility, value, nameof(NavigationSettingsVisibility));
        }

        public string HeaderSettingsGroup
        {
            get => _headerSettingsGroup;
            set => SetValue(ref _headerSettingsGroup, value, nameof(HeaderSettingsGroup));
        }

        public ICommand TaskbarIconDoubleClickCommand { get; }
        public ICommand TaskbarIconOpenCommand { get; }
        public ICommand TaskbarIconWriteReportCommand { get; }
        public ICommand TaskbarIconExitCommand { get; }

        public ICommand WindowClosingCommand { get; }
        public ICommand WindowOpenSettingsCommand { get; }

        public ICommand BackNavigateSettingsCommand { get; set; }
        public ICommand OpenGeneralSettingsCommand { get; }
        public ICommand OpenNotificationSettingsCommand { get; }
        public ICommand OpenManageProjectsSettingsCommand { get; }
        public ICommand OpenPersonalizationSettingsCommand { get; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            _settingsGroupVisibility = Visibility.Hidden;
            _navigationSettingsVisibility = Visibility.Hidden;
            _headerSettingsGroup = LocalizationHelper.GetLocalizedValue("Settings:Group_Settings");

            WindowVisibility = Environment.GetCommandLineArgs().Any(arg => arg == "/minimize")
                ? Visibility.Hidden
                : Visibility.Visible;

            TaskbarIconDoubleClickCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconOpenCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconWriteReportCommand = new RelayCommand(TaskbarIconWriteReportAction, true);
            TaskbarIconExitCommand = new RelayCommand(TaskbarIconExitAction, true);

            WindowClosingCommand = new RelayCommand(WindowClosingAction, true);
            WindowOpenSettingsCommand = new RelayCommand(WindowOpenSettingsAction, true);

            BackNavigateSettingsCommand = new RelayCommand(BackNavigateSettingsAction, true);
            OpenGeneralSettingsCommand = new RelayCommand(OpenGeneralSettingsAction, true);
            OpenManageProjectsSettingsCommand = new RelayCommand(OpenManageProjectsSettingsAction, true);
            OpenNotificationSettingsCommand = new RelayCommand(OpenNotificationSettingsAction, true);
            OpenPersonalizationSettingsCommand = new RelayCommand(OpenPersonalizationSettingsAction, true);
        }

        private void TaskbarIconOpenAction(object sender)
        {
            if (WindowState.Minimized == CurrentWindowState)
            {
                CurrentWindowState = _prevWindowState;
            }

            WindowVisibility = Visibility.Visible;
            Application.Current.MainWindow.Focus();
        }

        private void TaskbarIconWriteReportAction(object sender)
        {
            _navigationService.ShowNotificationWindow();
        }

        private void TaskbarIconExitAction(object sender)
        {
            Application.Current.Shutdown();
        }

        private void WindowClosingAction(object sender)
        {
            ((CancelEventArgs)sender).Cancel = true;
            WindowVisibility = Visibility.Hidden;
        }

        private void WindowOpenSettingsAction(object obj)
        {
            SettingsIsOpen = !SettingsIsOpen;
        }

        private void OpenSettingsPage<TViewModel>()
            where TViewModel : BaseViewModel
        {
            SettingsGroupVisibility = Visibility.Visible;
            NavigationSettingsVisibility = Visibility.Visible;

            var page = ServiceCollectionProvider.Container
                .GetInstance<TViewModel>();

            CurrentSettingsGroup = page;
        }

        private void OpenGeneralSettingsAction(object obj)
        {
            OpenSettingsPage<GeneralViewModel>();
            HeaderSettingsGroup = LocalizationHelper
                .GetLocalizedValue("Settings:Group_General");
        }

        private void OpenManageProjectsSettingsAction(object obj)
        {
            OpenSettingsPage<ManageProjectsViewModel>();
            HeaderSettingsGroup = LocalizationHelper
                .GetLocalizedValue("Settings:Group_ManageProjects");
        }

        private void OpenNotificationSettingsAction(object obj)
        {
            OpenSettingsPage<NotificationViewModel>();
            HeaderSettingsGroup = LocalizationHelper
                .GetLocalizedValue("Settings:Group_Notification");
        }

        private void OpenPersonalizationSettingsAction(object obj)
        {
            OpenSettingsPage<PersonalizationViewModel>();
            HeaderSettingsGroup = LocalizationHelper
                .GetLocalizedValue("Settings:Group_Personalization");
        }

        private void BackNavigateSettingsAction(object obj)
        {
            SettingsGroupVisibility = Visibility.Hidden;
            NavigationSettingsVisibility = Visibility.Hidden;

            CurrentSettingsGroup = null;
            HeaderSettingsGroup = LocalizationHelper
                .GetLocalizedValue("Settings:Group_Settings");
        }
        
    }
}
