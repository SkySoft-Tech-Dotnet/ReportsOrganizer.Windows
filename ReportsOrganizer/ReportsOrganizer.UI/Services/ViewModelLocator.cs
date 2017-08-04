using ReportsOrganizer.Core.Infrastructure;
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
    }
}
