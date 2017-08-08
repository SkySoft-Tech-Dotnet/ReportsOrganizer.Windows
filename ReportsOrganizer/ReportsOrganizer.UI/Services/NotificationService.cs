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
using ReportsOrganizer.Core.Services;

namespace ReportsOrganizer.UI.Services
{
    public interface INotificationService
    {
        void ShowNotificationWindow();
        void AddReport(string report);
    }
    public class NotificationService : INotificationService
    {
        INavigationService _navigationService;
        IReportsService _reportsService;

        public NotificationService(IReportsService reportsService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _reportsService = reportsService;
        }

        public void ShowNotificationWindow()
        {
            _navigationService.ShowNotificationWindow();
        }

        public void AddReport(string report)
        {
            _reportsService.Add(report);
        }
        
    }
}
