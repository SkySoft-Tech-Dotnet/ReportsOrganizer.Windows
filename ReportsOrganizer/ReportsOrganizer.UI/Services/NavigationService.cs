using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReportsOrganizer.UI.ViewModels;
using ReportsOrganizer.UI.Views;
using ReportsOrganizer.UI.Abstractions;

namespace ReportsOrganizer.UI.Services
{
    public interface INavigationService : INotifyPropertyChanged
    {
        BaseViewModel CurrentPage { get; }
        BaseViewModel PreviousPage { get; }

        void ShowNotificationWindow();
        void HideNotificationWindow();

        void NavigateToHome();
        void NavigateToSettings();
        void NavigateToPrevious();
    }

    class NavigationService : INavigationService
    {
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

        private BaseViewModel _previousPage;
        public BaseViewModel PreviousPage
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

        public void ShowNotificationWindow()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
        }

        public void HideNotificationWindow()
        {
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
