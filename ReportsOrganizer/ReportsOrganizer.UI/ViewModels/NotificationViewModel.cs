using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;
using ReportsOrganizer.UI.Abstractions;

namespace ReportsOrganizer.UI.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {
        INotificationService _notificationService;

        public ICommand NotificationWindowGetPreviousCommand { get; private set; }
        public ICommand NotificationWindowPostponeCommand { get; private set; }
        public ICommand NotificationWindowOKCommand { get; private set; }
        

        private string _multilineReportText;
        public string MultilineReportText
        {
            get
            {
                return _multilineReportText;
            }
            set
            {
                _multilineReportText = value;
                NotifyPropertyChanged(nameof(MultilineReportText));
            }
        }

        public NotificationViewModel(INotificationService notificationService)
        {
            NotificationWindowGetPreviousCommand = new AsyncCommand(NotificationWindowGetPreviousAction, (e) => { return true; });
            NotificationWindowPostponeCommand = new RelayCommand(NotificationWindowPostponeAction, true);
            NotificationWindowOKCommand = new RelayCommand(NotificationWindowOKAction, true);

            _notificationService = notificationService;
        }

        private async Task NotificationWindowGetPreviousAction(object sender)
        {
            var report  = await _notificationService.GetLastReport();
            MultilineReportText = report.Description;
        }

        private void NotificationWindowPostponeAction(object sender)
        {

        }

        private void NotificationWindowOKAction(object sender)
        {
            _notificationService.HideNotificationWindow();
            _notificationService.AddReport(MultilineReportText);
        }
    }
}
