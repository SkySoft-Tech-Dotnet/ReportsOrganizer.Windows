using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.UI.Extensions;
using ReportsOrganizer.UI.Models;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace ReportsOrganizer.UI
{
    public partial class App
    {
        private static readonly Mutex SingletonMutex =
            new Mutex(true, "{CA13A683-04A7-41E5-BFB1-43D22BADADB7}");

        private bool IsInDesignMode
            => DesignerProperties.GetIsInDesignMode(new DependencyObject());

        public App()
        {
            if (!SingletonMutex.WaitOne(TimeSpan.Zero, true) && !IsInDesignMode)
            {
                Current.Shutdown();
            }
        }

        protected override void OnStartup(StartupEventArgs startupEvent)
        {
            var startup = new Startup();

            startup.ConfigureServices(ServiceCollectionProvider.Container);
            ServiceCollectionProvider.Container.Verify();

            startup.Configure(ServiceCollectionProvider.Container);

            var applicationSettings = ServiceCollectionProvider.Container
                .GetInstance<IApplicationOptions<ApplicationSettings>>();

            ServiceCollectionProvider.Container.UseTheme(
                applicationSettings.Value.Personalization.Theme);

            base.OnStartup(startupEvent);
        }
    }
}
