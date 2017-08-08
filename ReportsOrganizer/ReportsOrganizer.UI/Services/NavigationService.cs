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
        private Lazy<NotificationView> _notificationView = new Lazy<NotificationView>(() => new NotificationView());
        private NotificationView NotificationViewInstance => _notificationView.Value;

        public NavigationService()
        {

        }

        public void ShowNotificationWindow()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            NotificationViewInstance.Left = desktopWorkingArea.Right - NotificationViewInstance.Width - 10;
            NotificationViewInstance.Top = desktopWorkingArea.Bottom - NotificationViewInstance.Height - 10;

            NotificationViewInstance.Show();
        }
    }
}
