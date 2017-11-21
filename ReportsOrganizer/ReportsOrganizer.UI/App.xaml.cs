using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.UI.Extensions;
using ReportsOrganizer.UI.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;

namespace ReportsOrganizer.UI
{
    public partial class App
    {
        private static readonly Mutex SingletonMutex = new Mutex(true, "{CA13A683-04A7-41E5-BFB1-43D22BADADB7}");
        private bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());

        public static string OpenEventHandle => "F4313BD4-32AB-48C6-9790-682F3B48C022";
        public static string NotificationEventHandle => "87A300C0-FFBC-42FE-A357-AE9DE1EAB5AE";

        public App()
        {
            if (Environment.GetCommandLineArgs().FirstOrDefault(arg => arg == "/notify") != null)
            {
                SetEventWaitHandle(NotificationEventHandle);
                Current.Shutdown();
            }
            if (!SingletonMutex.WaitOne(TimeSpan.Zero, true) && !IsInDesignMode)
            {
                SetEventWaitHandle(OpenEventHandle);
                Current.Shutdown();
            }
        }

        protected override void OnStartup(StartupEventArgs startupEvent)
        {
            var startup = new Startup();
            startup.ConfigureServices(ServiceCollectionProvider.Container);
            ServiceCollectionProvider.Container.Verify();

            var startupType = typeof(Startup);
            var methodInfo = startupType.GetMethod(nameof(startup.Configure));
            var parameters = methodInfo.GetParameters();

            methodInfo.Invoke(startup, parameters.Select(parameter
                => ServiceCollectionProvider.Container.GetInstance(parameter.ParameterType)).ToArray());

            base.OnStartup(startupEvent);
        }

        private void SetEventWaitHandle(string name)
        {
            if (EventWaitHandle.TryOpenExisting(name, out EventWaitHandle eventHandle))
            {
                eventHandle.Set();
            }
        }
    }
}
