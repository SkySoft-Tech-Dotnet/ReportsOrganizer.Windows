using ReportsOrganizer.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReportsOrganizer.UI.Services
{
    public interface INotificationService
    {
        void ShowNotificationWindow();
    }
    public class NotificationService : INotificationService
    {
        private NotificationView _notificationView;

        public NotificationService()
        {
            _notificationView = new NotificationView();
        }

        public void ShowNotificationWindow()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            _notificationView.Left = desktopWorkingArea.Right - _notificationView.Width - 10;
            _notificationView.Top = desktopWorkingArea.Bottom - _notificationView.Height - 10;
            
            _notificationView.Show();


            //var host = new Window();
            //host.Content = _notificationView;
            //host.Show();
        }
    }
}
