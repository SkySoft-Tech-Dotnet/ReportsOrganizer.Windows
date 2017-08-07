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

namespace ReportsOrganizer.UI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand TaskbarIconDoubleClickCommand { get; private set; }
        public ICommand TaskbarIconOpenCommand { get; private set; }
        public ICommand TaskbarIconReportCommand { get; private set; }
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

        public MainViewModel(IReportsService reportsService)
        {
            TaskbarIconDoubleClickCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconOpenCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconReportCommand = new RelayCommand(TaskbarIconReportAction, true);
            TaskbarIconExitCommand = new RelayCommand(TaskbarIconExitAction, true);
            WindowClosingCommand = new RelayCommand(WindowClosingAction, true);
        }

        private void TaskbarIconOpenAction(object sender)
        {
            if(WindowState.Minimized == CurrentWindowState)
            {
                CurrentWindowState = prevWindowState;
            }
            WindowVisibility = Visibility.Visible;
        }

        private void TaskbarIconReportAction(object sender)
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
    }
}
