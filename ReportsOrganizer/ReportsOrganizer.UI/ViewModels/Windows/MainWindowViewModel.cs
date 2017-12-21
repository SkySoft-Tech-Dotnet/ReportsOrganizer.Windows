using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.Localization.Helpers;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;
using ReportsOrganizer.UI.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using ReportsOrganizer.Core.Managers;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Helpers;
using ReportsOrganizer.UI.Managers;
using WPFLocalizeExtension.Engine;

namespace ReportsOrganizer.UI.ViewModels.Windows
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IExportService _exportService;
        private ApplicationManager _applicationManager;
        private readonly INotificationManager _notificationManager;

        private bool _settingsIsOpen;
        private string _headerSettingsGroupLocalizeKey;

        private Visibility _windowVisibility;
        private Visibility _settingsPageVisibility;

        private WindowState _prevWindowState;
        private WindowState _currentWindowState;

        private BaseViewModel _currentSettingsGroup;

        private Dictionary<string, AsyncCommand> _exportOptions;
        private DateTime _selectedDate;


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

        public string HeaderSettingsGroup
            => LocalizationHelper.GetLocalizedValue(HeaderSettingsGroupLocalizeKey);

        public string HeaderSettingsGroupLocalizeKey
        {
            get => _headerSettingsGroupLocalizeKey;
            set => SetValue(ref _headerSettingsGroupLocalizeKey, value, nameof(HeaderSettingsGroup));
        }

        public List<string> ExportOptions => _exportOptions.Keys.ToList();

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetValue(ref _selectedDate, value);
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

        public ICommand ExportCommand { get; }

        public MainWindowViewModel(
            ApplicationManager applicationManager,
            INotificationManager notificationManager,
            IExportService exportService)
        {
            _applicationManager = applicationManager;
            _notificationManager = notificationManager;
            _exportService = exportService;
            

            _settingsPageVisibility = Visibility.Hidden;
            _headerSettingsGroupLocalizeKey = "Settings:Group_Settings";

            WindowVisibility = Environment.GetCommandLineArgs().Any(arg => arg == "/minimized")
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

            ExportCommand = new RelayCommand(ExportAction, true);

            _exportOptions = new Dictionary<string, AsyncCommand>
            {
                {"Export to csv", new AsyncCommand(ExportMonthReportAction) },
                {"Export to google", new AsyncCommand(ExportMonthReportAction) },
                {"Export all", new AsyncCommand(ExportAllAction) }
            };

            SelectedDate = DateTime.Now;

            LocalizeDictionary.Instance.PropertyChanged += LocalizeChanged;

            _notificationManager.Notified += delegate
            {
                _applicationManager.NotificationWindow.Show();
            };
        }

        ~MainWindowViewModel()
        {
            LocalizeDictionary.Instance.PropertyChanged -= LocalizeChanged;
        }

        private void TaskbarIconOpenAction(object sender)
        {
            Activate();
        }

        private void TaskbarIconWriteReportAction(object obj)
        {
            _applicationManager.NotificationWindow.Show();
            //_applicationManager.NotificationWindow.Visibility = Visibility.Visible;
            //_navigationService.ShowNotificationWindow();
        }

        private void TaskbarIconExitAction(object obj)
        {
            Application.Current.Shutdown();
        }

        private void WindowClosingAction(object obj)
        {
            ((CancelEventArgs)obj).Cancel = true;
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

            var page = ServiceCollectionProvider.Container
                .GetInstance<TViewModel>();

            CurrentSettingsGroup = page;

            //var pageWithLoad = page as INavigateble;
            //pageWithLoad.OnNavigate();
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

            if (CurrentSettingsGroup != null)
                CurrentSettingsGroup = null;
            else
                WindowOpenSettingsAction(null);

            HeaderSettingsGroupLocalizeKey = "Settings:Group_Settings";
        }

        private void ExportAction(object obj)
        {
            if(obj is string option)
                _exportOptions[option].Execute(null);
        }

        private async Task ExportMonthReportAction(object obj)
        {
            var fileDialog = new SaveFileDialog
            {
                FileName = "MonthReports",
                DefaultExt = ".csv",
                Filter = "Spreadsheet documents|*.csv"
            };

            if (fileDialog.ShowDialog() == true)
                await _exportService.WriteMonthReport(SelectedDate.Year, SelectedDate.Month, fileDialog.FileName, CancellationToken.None);
        }

        private async Task ExportAllAction(object obj)
        {
            var fileDialog = new SaveFileDialog
            {
                FileName = "AllReports",
                DefaultExt = ".csv",
                Filter = "Spreadsheet documents|*.csv"
            };

            if (fileDialog.ShowDialog() == true)
                await _exportService.WriteAll(fileDialog.FileName, CancellationToken.None);
        }

        public void Activate()
        {
            if (CurrentWindowState == WindowState.Minimized)
            {
                CurrentWindowState = _prevWindowState;
            }

            WindowVisibility = Visibility.Visible;
            Application.Current.MainWindow?.Activate();
        }

        private void LocalizeChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(HeaderSettingsGroup));
        }
    }
}
