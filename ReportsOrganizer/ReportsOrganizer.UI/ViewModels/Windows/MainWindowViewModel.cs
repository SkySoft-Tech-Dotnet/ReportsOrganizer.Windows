using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.ViewModels.Settings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ReportsOrganizer.UI.ViewModels.Windows
{
    public class MainWindowViewModel : BaseViewModel
    {
        private bool _settingsIsOpen;

        private Visibility _windowVisibility;
        private Visibility _groupSettingsVisibility;
        private Visibility _navigationSettingsVisibility;

        private WindowState _prevWindowState;
        private WindowState _currentWindowState;

        private BaseViewModel _currentSettingsPage;

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

        public BaseViewModel CurrentSettingsPage
        {
            get => _currentSettingsPage;
            set => SetValue(ref _currentSettingsPage, value, nameof(CurrentSettingsPage));
        }

        public Visibility GroupSettingsVisibility
        {
            get => _groupSettingsVisibility;
            set => SetValue(ref _groupSettingsVisibility, value, nameof(GroupSettingsVisibility));
        }

        public Visibility NavigationSettingsVisibility
        {
            get => _navigationSettingsVisibility;
            set => SetValue(ref _navigationSettingsVisibility, value, nameof(NavigationSettingsVisibility));
        }

        public Stack<BaseViewModel> SettingsNavigation { get; }

        public ICommand TaskbarIconDoubleClickCommand { get; }
        public ICommand TaskbarIconOpenCommand { get; }
        public ICommand TaskbarIconWriteReportCommand { get; }
        public ICommand TaskbarIconExitCommand { get; }

        public ICommand WindowClosingCommand { get; }
        public ICommand WindowOpenSettingsCommand { get; }

        public ICommand BackNavigateSettingsCommand { get; set; }
        public ICommand OpenGeneralSettingsCommand { get; }

        public MainWindowViewModel()
        {
            _groupSettingsVisibility = Visibility.Visible;
            _navigationSettingsVisibility = Visibility.Hidden;

            SettingsNavigation = new Stack<BaseViewModel>();

            TaskbarIconDoubleClickCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconOpenCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconWriteReportCommand = new RelayCommand(TaskbarIconWriteReportAction, true);
            TaskbarIconExitCommand = new RelayCommand(TaskbarIconExitAction, true);

            WindowClosingCommand = new RelayCommand(WindowClosingAction, true);
            WindowOpenSettingsCommand = new RelayCommand(WindowOpenSettingsAction, true);

            BackNavigateSettingsCommand = new RelayCommand(BackNavigateSettingsAction, true);
            OpenGeneralSettingsCommand = new RelayCommand(OpenGeneralSettingsAction, true);
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
            WindowVisibility = Visibility.Collapsed;
        }

        private void WindowOpenSettingsAction(object obj)
        {
            SettingsIsOpen = !SettingsIsOpen;
        }

        private void OpenGeneralSettingsAction(object obj)
        {
            GroupSettingsVisibility = Visibility.Collapsed;
            NavigationSettingsVisibility = Visibility.Visible;

            var page = ServiceCollectionProvider.Container
                .GetInstance<GeneralSettingsViewModel>();

            CurrentSettingsPage = page;
            SettingsNavigation.Push(page);
        }

        private void BackNavigateSettingsAction(object obj)
        {
            SettingsNavigation.Pop();
            if (SettingsNavigation.Count == 0)
            {
                GroupSettingsVisibility = Visibility.Visible;
                NavigationSettingsVisibility = Visibility.Hidden;
                CurrentSettingsPage = null;
            }
            else
            {
                CurrentSettingsPage = SettingsNavigation.Peek();
            }
        }
        
    }
}
