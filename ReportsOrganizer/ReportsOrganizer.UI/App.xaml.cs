using ReportsOrganizer.Core.Infrastructure;
using ReportsOrganizer.UI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;

namespace ReportsOrganizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex mutex = new Mutex(true, "{CA13A683-04A7-41E5-BFB1-43D22BADADB7}");

        public App()
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                Current.Shutdown();
            }
            IoCConfiguration();
        }

        public void IoCConfiguration()
        {
            IoC.Container.Register<INotificationService, NotificationService>();
        }
    }
}
