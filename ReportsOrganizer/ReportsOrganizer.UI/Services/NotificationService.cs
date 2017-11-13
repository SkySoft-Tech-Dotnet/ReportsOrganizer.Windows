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
using ReportsOrganizer.Models;

namespace ReportsOrganizer.UI.Services
{
    public interface INotificationService
    {
        void ShowNotificationWindow();
        void HideNotificationWindow();
        void AddReport(string report);
        Task<Report> GetLastReport();
    }
    public class NotificationService : INotificationService
    {
        INavigationService _navigationService;
        IReportService _reportService;

        public NotificationService(IReportService reportService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _reportService = reportService;
        }

        public void ShowNotificationWindow()
        {
            _navigationService.ShowNotificationWindow();
        }

        public void HideNotificationWindow()
        {
            _navigationService.HideNotificationWindow();
        }

        public void AddReport(string report)
        {
            _reportService.Add(report);
        }

        public Task<Report> GetLastReport()
        {
            return _reportService.GetLastReport();
        }
        
    }
}
