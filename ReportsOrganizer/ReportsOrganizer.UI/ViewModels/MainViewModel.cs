using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Services;

namespace ReportsOrganizer.UI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        INotificationService _notificationService;
        IReportsService _reportsService;
        public ICommand TaskbarIconDoubleClickCommand { get; private set; }
        public ICommand TaskbarIconOpenCommand { get; private set; }
        public ICommand TaskbarIconWriteReportCommand { get; private set; }
        public ICommand TaskbarIconExitCommand { get; private set; }
        public ICommand WindowClosingCommand { get; private set; }
        
        private Visibility windowVisibility;
        public Visibility WindowVisibility
        {
            get
            {
                return windowVisibility;
            }
            set
            {
                windowVisibility = value;
                NotifyPropertyChanged(nameof(WindowVisibility));
            }
        }

        private WindowState prevWindowState;
        private WindowState currentWindowState;
        public WindowState CurrentWindowState
        {
            get
            {
                return currentWindowState;
            }
            set
            {
                prevWindowState = currentWindowState;
                currentWindowState = value;
                NotifyPropertyChanged(nameof(CurrentWindowState));
            }
        }

        public MainViewModel(IReportsService reportsService, INotificationService notificationService)
        {
            TaskbarIconDoubleClickCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconOpenCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconWriteReportCommand = new RelayCommand(TaskbarIconWriteReportAction, true);
            TaskbarIconExitCommand = new RelayCommand(TaskbarIconExitAction, true);
            WindowClosingCommand = new RelayCommand(WindowClosingAction, true);

            

            _reportsService = reportsService;
            _notificationService = notificationService;
        }

        private void TaskbarIconOpenAction(object sender)
        {
            if(WindowState.Minimized == CurrentWindowState)
            {
                CurrentWindowState = prevWindowState;
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
    }
}
