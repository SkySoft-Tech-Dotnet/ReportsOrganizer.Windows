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

        public MainViewModel(IReportsService reportsService)
        {
            TaskbarIconOpenCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconReportCommand = new RelayCommand(TaskbarIconReportAction, true);
            TaskbarIconExitCommand = new RelayCommand(TaskbarIconExitAction, true);
            WindowClosingCommand = new RelayCommand(WindowClosingAction, true);

            windowVisibility = Visibility.Visible;
        }

        private void TaskbarIconOpenAction(object sender)
        {
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
