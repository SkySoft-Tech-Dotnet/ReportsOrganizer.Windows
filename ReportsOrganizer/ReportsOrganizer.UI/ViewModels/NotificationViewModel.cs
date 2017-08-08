﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;

namespace ReportsOrganizer.UI.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {
        INotificationService _notificationService;

        public ICommand NotificationWindowGetPreviousCommand { get; private set; }
        public ICommand NotificationWindowPostponeCommand { get; private set; }
        public ICommand NotificationWindowOKCommand { get; private set; }

        public string MultilineReportText { get; set; }


        public NotificationViewModel(INotificationService notificationService)
        {
            NotificationWindowGetPreviousCommand = new RelayCommand(NotificationWindowGetPreviousAction, true);
            NotificationWindowPostponeCommand = new RelayCommand(NotificationWindowPostponeAction, true);
            NotificationWindowOKCommand = new RelayCommand(NotificationWindowOKAction, true);

            _notificationService = notificationService;
        }

        private void NotificationWindowGetPreviousAction(object sender)
        {

        }

        private void NotificationWindowPostponeAction(object sender)
        {

        }

        private void NotificationWindowOKAction(object sender)
        {
            _notificationService.AddReport(MultilineReportText);
        }
    }
}
