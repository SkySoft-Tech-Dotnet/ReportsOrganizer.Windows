using ReportsOrganizer.UI.ViewModel;
using ReportsOrganizer.UI.ViewModels;
using ReportsOrganizer.UI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReportsOrganizer.UI.Services
{
    public interface INavigationService : INotifyPropertyChanged
    {
        BaseViewModel CurrentPage { get; }

        void ShowNotificationWindow();

        void NavigateToHome();
        void NavigateToSettings();
    }

    class NavigationService : INavigationService
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Lazy<NotificationView> _notificationView = new Lazy<NotificationView>(() => new NotificationView());
        private NotificationView NotificationViewInstance => _notificationView.Value;

        private BaseViewModel currentPage;
        public BaseViewModel CurrentPage
        {
            get
            {
                return currentPage;
            }
            private set
            {
                currentPage = value;
                NotifyPropertyChanged(nameof(CurrentPage));
            }
        }

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

        public void NavigateToHome()
        {
            CurrentPage = Core.Infrastructure.IoC.Container.GetInstance<HomeViewModel>();
        }

        public void NavigateToSettings()
        {
            CurrentPage = Core.Infrastructure.IoC.Container.GetInstance<SettingsViewModel>();
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
