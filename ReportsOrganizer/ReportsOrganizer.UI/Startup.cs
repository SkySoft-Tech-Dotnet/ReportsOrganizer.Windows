using ReportsOrganizer.Core.Extensions;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DI.Extensions;
using ReportsOrganizer.UI.Models;
using ReportsOrganizer.UI.Services;
using ReportsOrganizer.UI.ViewModels.Settings;
using ReportsOrganizer.UI.ViewModels.Windows;
using SimpleInjector;
using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace ReportsOrganizer.UI
{
    public class Startup
    {
        public void ConfigureServices(Container container)
        {
            container.AddConfiguration<ApplicationSettings>("appsettings.json");

            container.AddSingleton<INotificationService, NotificationService>();
            container.AddSingleton<INavigationService, NavigationService>();

            container.AddTransient<MainWindowViewModel>();

            container.AddTransient<GeneralSettingsViewModel>();

            container.AddCore();



            //container.AddTransient<IHomeViewModel, HomeViewModel>();
            //container.Register<INotificationService, NotificationService>(Lifestyle.Singleton);
            //container.Register<INavigationService, NavigationService>(Lifestyle.Singleton);

            //container.Register<IHomeViewModel, HomeViewModel>();
            //container.Register<ISettingsViewModel, SettingsViewModel>();
            //container.Register<IScheduleService, ScheduleService>();
        }

        public void Configure(Container container)
        {
            var applicationSettings = container
                .GetInstance<IApplicationOptions<ApplicationSettings>>().Value;

            LocalizeDictionary.Instance.Culture = new CultureInfo(applicationSettings.General.Language);
        }
    }
}
