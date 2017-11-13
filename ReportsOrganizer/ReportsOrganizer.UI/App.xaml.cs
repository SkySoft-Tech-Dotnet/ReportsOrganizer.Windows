using ReportsOrganizer.DI.Providers;
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

        private readonly DependencyObject _dummy = new DependencyObject();

        private bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(_dummy);

        public App()
        {
            if (!SingletonMutex.WaitOne(TimeSpan.Zero, true) && !IsInDesignMode)
            {
                Current.Shutdown();
            }

            var startup = new Startup();

            startup.ConfigureServices(ServiceCollectionProvider.Container);
            ServiceCollectionProvider.Container.Verify();

            startup.Configure(ServiceCollectionProvider.Container);
        }
    }
}
