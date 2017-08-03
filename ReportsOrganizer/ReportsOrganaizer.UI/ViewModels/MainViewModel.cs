using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ReportsOrganaizer.UI.Command;

namespace ReportsOrganaizer.UI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand OpenCommand
        {
            get
            {
                return new RelayCommand(OpenAction, true);
            }
        }

        public ICommand WriteReportCommand
        {
            get
            {
                return new RelayCommand(WriteReportAction, true);
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                return new RelayCommand(ExitAction, true);
            }
        }

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
            windowVisibility = Visibility.Visible;

            Application.Current.MainWindow.Closing += new CancelEventHandler((sender, e) => {
                WindowVisibility = Visibility.Hidden;
                e.Cancel = true;
            });
        }

        private void OpenAction(object sender)
        {
            WindowVisibility = Visibility.Visible;
        }

        private void WriteReportAction(object sender)
        {

        }

        private void ExitAction(object sender)
        {
            Application.Current.Shutdown();
        }
    }
}
