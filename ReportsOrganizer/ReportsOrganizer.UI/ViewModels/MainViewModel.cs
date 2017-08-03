using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ReportsOrganizer.UI.Command;

namespace ReportsOrganizer.UI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand TaskbarIconOpenCommand { get; private set; }
        public ICommand TaskbarIconReportCommand { get; private set; }
        public ICommand TaskbarIconExitCommand { get; private set; }

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

        public MainViewModel()
        {
            TaskbarIconOpenCommand = new RelayCommand(TaskbarIconOpenAction, true);
            TaskbarIconReportCommand = new RelayCommand(TaskbarIconReportAction, true);
            TaskbarIconExitCommand = new RelayCommand(TaskbarIconExitAction, true);

            windowVisibility = Visibility.Visible;

            Application.Current.MainWindow.Closing += new CancelEventHandler((sender, e) =>
            {
                WindowVisibility = Visibility.Hidden;
                e.Cancel = true;
            });
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
    }
}
