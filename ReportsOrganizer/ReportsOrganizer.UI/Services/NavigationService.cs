using ReportsOrganizer.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReportsOrganizer.UI.Services
{
    public interface INavigationService
    {
        void ShowNotificationWindow();
    }
    class NavigationService : INavigationService
    {
        private NotificationView _notificationView;

        public NavigationService()
        {
            _notificationView = new NotificationView();
        }

        public void ShowNotificationWindow()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            _notificationView.Left = desktopWorkingArea.Right - _notificationView.Width - 10;
            _notificationView.Top = desktopWorkingArea.Bottom - _notificationView.Height - 10;

            _notificationView.Show();
        }
    }
}
