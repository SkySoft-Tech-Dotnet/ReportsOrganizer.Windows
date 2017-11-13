using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReportsOrganizer.UI.ViewModels;
using ReportsOrganizer.UI.Views;

namespace ReportsOrganizer.UI.Services
{
    public interface INavigationService : INotifyPropertyChanged
    {
        BaseViewModel2 CurrentPage { get; }
        BaseViewModel2 PreviousPage { get; }

        void ShowNotificationWindow();
        void HideNotificationWindow();

        void NavigateToHome();
        void NavigateToSettings();
        void NavigateToPrevious();
    }

    class NavigationService : INavigationService
    {
        private Lazy<NotificationView> _notificationView;

        private NotificationView NotificationViewInstance => _notificationView.Value;

        private BaseViewModel2 currentPage;
        public BaseViewModel2 CurrentPage
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

        private BaseViewModel2 _previousPage;
        public BaseViewModel2 PreviousPage
        {
            get
            {
                return _previousPage;
            }
            private set
            {
                _previousPage = value;
                NotifyPropertyChanged(nameof(PreviousPage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public NavigationService()
        {
            _notificationView = new Lazy<NotificationView>(() => new NotificationView());
        }

        public void ShowNotificationWindow()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            NotificationViewInstance.Left = desktopWorkingArea.Right - NotificationViewInstance.Width - 10;
            NotificationViewInstance.Top = desktopWorkingArea.Bottom - NotificationViewInstance.Height - 10;

            NotificationViewInstance.Report.Text = null;
            NotificationViewInstance.Show();
        }

        public void HideNotificationWindow()
        {
            NotificationViewInstance.Hide();
        }

        public void NavigateToHome()
        {
            PreviousPage = CurrentPage;
            //CurrentPage = Core.Infrastructure.IoC.Container.GetInstance<HomeViewModel>();
        }

        public void NavigateToSettings()
        {
            PreviousPage = CurrentPage;
            //CurrentPage = Core.Infrastructure.IoC.Container.GetInstance<SettingsViewModel>();
        }

        public void NavigateToPrevious()
        {
            CurrentPage = PreviousPage;
            PreviousPage = null;
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
