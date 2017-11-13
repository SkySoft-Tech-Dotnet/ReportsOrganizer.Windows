using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ReportsOrganizer.UI.ViewModels
{
    public class MainViewModel : BaseViewModel2
    {
        public ICommand OpenSettingsCommand { get; private set; }

        private bool _settingsIsOpen;
        public bool SettingsIsOpen
        {
            get
            {
                return _settingsIsOpen;
            }
            set
            {
                _settingsIsOpen = value;
                NotifyPropertyChanged(nameof(SettingsIsOpen));
            }
        }

        private void OpenSettingsAction(object sender)
        {
            SettingsIsOpen = !_settingsIsOpen;
        }







        INotificationService _notificationService;
        IReportService _reportService;

        public ICommand TaskbarIconDoubleClickCommand { get; private set; }
        public ICommand TaskbarIconOpenCommand { get; private set; }
        public ICommand TaskbarIconWriteReportCommand { get; private set; }
        public ICommand TaskbarIconExitCommand { get; private set; }

        public ICommand WindowClosingCommand { get; private set; }
        public ICommand WindowOpenSettingsCommand { get; private set; }
        public ICommand WindowComeBackCommand { get; private set; }

        private Visibility _windowVisibility;
        public Visibility WindowVisibility
        {
            get
            {
                return _windowVisibility;
            }
            set
            {
                _windowVisibility = value;
                NotifyPropertyChanged(nameof(WindowVisibility));
            }
        }

        private WindowState _prevWindowState;
        private WindowState _currentWindowState;
        public WindowState CurrentWindowState
        {
            get
            {
                return _currentWindowState;
            }
            set
            {
                _prevWindowState = _currentWindowState;
                _currentWindowState = value;
                NotifyPropertyChanged(nameof(CurrentWindowState));
            }
        }

        public INavigationService Navigation { get; private set; }

        private bool _isVisibleIconOpenSetting;
        public bool IsVisibleIconOpenSetting
        {
            get
            {
                return _isVisibleIconOpenSetting;
            }
            private set
            {
                _isVisibleIconOpenSetting = value;
                NotifyPropertyChanged(nameof(IsVisibleIconOpenSetting));
            }
        }

        private bool _isVisibleIconPrev;
        public bool IsVisibleIconPrev
        {
            get
            {
                return _isVisibleIconPrev;
            }
            private set
            {
                _isVisibleIconPrev = value;
                NotifyPropertyChanged(nameof(IsVisibleIconPrev));
            }
        }

        public MainViewModel(IReportService reportService, INotificationService notificationService, INavigationService navigationService)
        {
            OpenSettingsCommand = new RelayCommand(OpenSettingsAction, true);


            TaskbarIconDoubleClickCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconOpenCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconWriteReportCommand = new RelayCommand(TaskbarIconWriteReportAction, true);
            TaskbarIconExitCommand = new RelayCommand(TaskbarIconExitAction, true);

            WindowClosingCommand = new RelayCommand(WindowClosingAction, true);
            WindowOpenSettingsCommand = new RelayCommand(WindowOpenSettingsAction, true);
            WindowComeBackCommand = new RelayCommand(WindowComeBackAction, true);

            Navigation = navigationService;
            Navigation.NavigateToHome();

            IsVisibleIconOpenSetting = true;
            IsVisibleIconPrev = false;

            _reportService = reportService;
            _notificationService = notificationService;
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
            _notificationService.ShowNotificationWindow();
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
            Navigation.NavigateToSettings();

            IsVisibleIconOpenSetting = false;
            IsVisibleIconPrev = true;
        }
        
        private void WindowComeBackAction(object obj)
        {
            Navigation.NavigateToPrevious();

            IsVisibleIconOpenSetting = true;
            IsVisibleIconPrev = false;
        }
    }
}
