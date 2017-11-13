using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.ViewModels.Settings;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ReportsOrganizer.UI.ViewModels.Windows
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Visibility _windowVisibility;
        private WindowState _prevWindowState;
        private WindowState _currentWindowState;
        private bool _settingsIsOpen;
        
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

        public BaseViewModel SettingsViewModel { get; }
        
        public ICommand TaskbarIconDoubleClickCommand { get; }
        public ICommand TaskbarIconOpenCommand { get; }
        public ICommand TaskbarIconWriteReportCommand { get; }
        public ICommand TaskbarIconExitCommand { get; }

        public ICommand WindowClosingCommand { get; }
        public ICommand WindowOpenSettingsCommand { get; }

        public MainWindowViewModel()
        {
            SettingsViewModel = new SettingsViewModel();

            TaskbarIconDoubleClickCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconOpenCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconWriteReportCommand = new RelayCommand(TaskbarIconWriteReportAction, true);
            TaskbarIconExitCommand = new RelayCommand(TaskbarIconExitAction, true);

            WindowClosingCommand = new RelayCommand(WindowClosingAction, true);
            WindowOpenSettingsCommand = new RelayCommand(WindowOpenSettingsAction, true);
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
            SettingsIsOpen = !_settingsIsOpen;
        }
    }
}
