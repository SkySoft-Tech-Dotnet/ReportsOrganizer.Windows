using MahApps.Metro;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DI.Providers;
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

            var startup = new Startup();

            startup.ConfigureServices(ServiceCollectionProvider.Container);
            ServiceCollectionProvider.Container.Verify();

            startup.Configure(ServiceCollectionProvider.Container);
        }

        protected override void OnStartup(StartupEventArgs startupEvent)
        {
            var themes = ServiceCollectionProvider.Container
                .GetInstance<ApplicationTheme>()?.Themes;

            var applicationSettings = ServiceCollectionProvider.Container
                .GetInstance<IApplicationOptions<ApplicationSettings>>();

            if (themes != null)
            {
                var theme = themes.FirstOrDefault(themeItem
                    => themeItem.Key.Equals(
                        applicationSettings.Value.Personalization.Theme,
                        StringComparison.OrdinalIgnoreCase));

                ThemeManager.AddAccent(applicationSettings.Value.Personalization.Theme, theme.Value);

                Tuple<AppTheme, Accent> appTheme = ThemeManager.DetectAppStyle(Current);
                Accent appAccent = ThemeManager.GetAccent(applicationSettings.Value.Personalization.Theme);

                ThemeManager.ChangeAppStyle(Current, appAccent, appTheme.Item1);
            }

            base.OnStartup(startupEvent);
        }
    }
}
