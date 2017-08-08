using ReportsOrganizer.Core.Infrastructure;
using ReportsOrganizer.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOrganizer.UI.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel Main
        {
            get { return IoC.Container.GetInstance<MainViewModel>(); }
        }

        public NotificationViewModel Notification
        {
            get { return IoC.Container.GetInstance<NotificationViewModel>(); }
        }

        public HomeViewModel HomePage
        {
            get { return IoC.Container.GetInstance<HomeViewModel>(); }
        }

        public SettingsViewModel SettingsPage
        {
            get { return IoC.Container.GetInstance<SettingsViewModel>(); }
        }
    }
}
