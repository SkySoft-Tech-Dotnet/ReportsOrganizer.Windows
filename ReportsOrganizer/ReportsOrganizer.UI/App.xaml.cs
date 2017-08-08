using ReportsOrganizer.Core.Infrastructure;
using ReportsOrganizer.UI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Globalization;
using WPFLocalizeExtension.Engine;
using SimpleInjector;

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
            LocalizeDictionary.Instance.Culture = new CultureInfo("en");
        }

        public void IoCConfiguration()
        {
            IoC.Container.Register<INotificationService, NotificationService>(Lifestyle.Singleton);
            IoC.Container.Register<INavigationService, NavigationService>(Lifestyle.Singleton);
        }
    }
}
