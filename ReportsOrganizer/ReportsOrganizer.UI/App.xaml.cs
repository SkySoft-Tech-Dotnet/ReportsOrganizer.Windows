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
using ReportsOrganizer.UI.ViewModels;
using SimpleInjector;
using System.ComponentModel;
using ReportsOrganizer.UI.Models;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.Core.Extensions;

namespace ReportsOrganizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex = new Mutex(true, "{CA13A683-04A7-41E5-BFB1-43D22BADADB7}");

        private readonly DependencyObject _dummy = new DependencyObject();

        private bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(_dummy);

        public App()
        {
            if (!_mutex.WaitOne(TimeSpan.Zero, true) && !IsInDesignMode)
            {
                Current.Shutdown();
            }

            LocalizeDictionary.Instance.Culture = new CultureInfo("en");
            IoCConfiguration();
        }

        public void IoCConfiguration()
        {
            IoC.Container.AddConfiguration<ApplicationSettings>("appsettings.json");

            IoC.Container.Register<INotificationService, NotificationService>(Lifestyle.Singleton);
            IoC.Container.Register<INavigationService, NavigationService>(Lifestyle.Singleton);

            IoC.Container.Register<IHomeViewModel, HomeViewModel>();
            IoC.Container.Register<ISettingsViewModel, SettingsViewModel>();
        }
    }
}
