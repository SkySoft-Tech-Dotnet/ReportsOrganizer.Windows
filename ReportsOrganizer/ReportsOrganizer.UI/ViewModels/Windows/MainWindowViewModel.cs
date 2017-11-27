using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.Localization.Helpers;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;
using ReportsOrganizer.UI.ViewModels.Settings;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ReportsOrganizer.UI.Managers;
using WPFLocalizeExtension.Engine;

namespace ReportsOrganizer.UI.ViewModels.Windows
{
    public class MainWindowViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        private bool _settingsIsOpen;
        private string _headerSettingsGroupLocalizeKey;

        private Visibility _windowVisibility;
        private Visibility _settingsPageVisibility;
        private Visibility _settingsBackButtonVisibility;

        private WindowState _prevWindowState;
        private WindowState _currentWindowState;

        private BaseViewModel _currentSettingsGroup;

        private ApplicationManager _applicationManager;

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

        public Visibility SettingsPageVisibility
        {
            get => _settingsPageVisibility;
            set => SetValue(ref _settingsPageVisibility, value, nameof(SettingsPageVisibility));
        }

        public Visibility SettingsBackButtonVisibility
        {
            get => _settingsBackButtonVisibility;
            set => SetValue(ref _settingsBackButtonVisibility, value, nameof(SettingsBackButtonVisibility));
        }

        public string HeaderSettingsGroup
            => LocalizationHelper.GetLocalizedValue(HeaderSettingsGroupLocalizeKey);

        public string HeaderSettingsGroupLocalizeKey
        {
            get => _headerSettingsGroupLocalizeKey;
            set => SetValue(ref _headerSettingsGroupLocalizeKey, value, nameof(HeaderSettingsGroup));
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

        public MainWindowViewModel(
            ApplicationManager applicationManager,
            INavigationService navigationService)
        {
            _applicationManager = applicationManager;
            _navigationService = navigationService;

            _settingsPageVisibility = Visibility.Hidden;
            _settingsBackButtonVisibility = Visibility.Hidden;
            _headerSettingsGroupLocalizeKey = "Settings:Group_Settings";

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

            LocalizeDictionary.Instance.PropertyChanged += LocalizeChanged;

            new Task(() =>
            {
                var handle = new EventWaitHandle(true, EventResetMode.AutoReset, App.OpenEventHandle);
                while (true)
                {
                    handle.WaitOne();

                    if (WindowState.Minimized == CurrentWindowState)
                    {
                        CurrentWindowState = _prevWindowState;
                    }

                    WindowVisibility = Visibility.Visible;
                }
            }).Start();
        }

        ~MainWindowViewModel()
        {
            LocalizeDictionary.Instance.PropertyChanged -= LocalizeChanged;
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
            _applicationManager.NotificationWindow.Show();
            //_applicationManager.NotificationWindow.Visibility = Visibility.Visible;
            //_navigationService.ShowNotificationWindow();
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
            SettingsPageVisibility = Visibility.Visible;
            SettingsBackButtonVisibility = Visibility.Visible;

            var page = ServiceCollectionProvider.Container
                .GetInstance<TViewModel>();

            CurrentSettingsGroup = page;
        }

        private void OpenGeneralSettingsAction(object obj)
        {
            OpenSettingsPage<GeneralViewModel>();
            HeaderSettingsGroupLocalizeKey = "Settings:Group_General";
        }

        private void OpenManageProjectsSettingsAction(object obj)
        {
            OpenSettingsPage<ManageProjectsViewModel>();
            HeaderSettingsGroupLocalizeKey = "Settings:Group_ManageProjects";
        }

        private void OpenNotificationSettingsAction(object obj)
        {
            OpenSettingsPage<Settings.NotificationViewModel>();
            HeaderSettingsGroupLocalizeKey = "Settings:Group_Notification";
        }

        private void OpenPersonalizationSettingsAction(object obj)
        {
            OpenSettingsPage<PersonalizationViewModel>();
            HeaderSettingsGroupLocalizeKey = "Settings:Group_Personalization";
        }

        private void BackNavigateSettingsAction(object obj)
        {
            SettingsPageVisibility = Visibility.Hidden;
            SettingsBackButtonVisibility = Visibility.Hidden;

            CurrentSettingsGroup = null;
            HeaderSettingsGroupLocalizeKey = "Settings:Group_Settings";
        }

        private void LocalizeChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(HeaderSettingsGroup));
        }
    }
}
