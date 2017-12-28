using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.UI.Extensions;
using ReportsOrganizer.UI.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using ReportsOrganizer.Core.Managers;
using ReportsOrganizer.UI.Helpers;
using ReportsOrganizer.UI.Managers;
using ReportsOrganizer.UI.Services;
using ReportsOrganizer.UI.ViewModels.Windows;

namespace ReportsOrganizer.UI
{
    public partial class App
    {
        public App()
        {
            CrossThreadManager.Instance.ActivateApplication += OnActivateApplication;
            CrossThreadManager.Instance.ActiveInstanceDetected += OnActiveInstanceDetected;
            CrossThreadManager.Instance.ShowNotifyForm += OnShowNotifyForm;

            CrossThreadManager.Instance.HandleApplicationStart(Environment.GetCommandLineArgs());
        }

        private void OnActivateApplication(object sender, EventArgs eventArgs)
        {
            var mainViewModel = ServiceCollectionProvider.Container.GetInstance<MainWindowViewModel>();
            mainViewModel.ActivateMainWindow();
        }

        private void OnActiveInstanceDetected(object sender, EventArgs eventArgs)
        {
            Current.Shutdown();
        }

        private void OnShowNotifyForm(object sender, EventArgs eventArgs)
        {
            var notificationManager = ServiceCollectionProvider.Container.GetInstance<INotificationManager>();
            notificationManager.Notify();
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
    }
}
