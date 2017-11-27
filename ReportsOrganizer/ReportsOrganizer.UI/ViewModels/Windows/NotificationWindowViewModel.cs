using ReportsOrganizer.UI.Abstractions;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ReportsOrganizer.UI.Command;

namespace ReportsOrganizer.UI.ViewModels.Windows
{
    public class NotificationWindowViewModel : BaseViewModel
    {
        private Visibility _windowVisibility;

        public ICommand WindowClosingCommand { get; }

        public Visibility WindowVisibility
        {
            get => _windowVisibility;
            set => SetValue(ref _windowVisibility, value, nameof(WindowVisibility));
        }

        public NotificationWindowViewModel()
        {
            WindowClosingCommand = new RelayCommand(WindowClosingAction, true);
        }

        private void WindowClosingAction(object sender)
        {
            ((CancelEventArgs)sender).Cancel = true;
            WindowVisibility = Visibility.Hidden;
        }
    }
}
