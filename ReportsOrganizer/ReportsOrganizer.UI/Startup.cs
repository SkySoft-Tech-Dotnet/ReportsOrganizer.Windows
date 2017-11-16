using ReportsOrganizer.Core.Extensions;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DI.Extensions;
using ReportsOrganizer.UI.Extensions;
using ReportsOrganizer.UI.Models;
using ReportsOrganizer.UI.Services;
using ReportsOrganizer.UI.ViewModels.Settings;
using ReportsOrganizer.UI.ViewModels.Windows;
using SimpleInjector;
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using WPFLocalizeExtension.Engine;

namespace ReportsOrganizer.UI
{
    public class Startup
    {
        public void ConfigureServices(Container container)
        {
            container.AddConfiguration<ApplicationSettings>("appsettings.json");

            container.AddTransient<MainWindowViewModel>();
            container.AddTransient<GeneralViewModel>();


            
            container.AddSingleton<INotificationService, NotificationService>();
            container.AddSingleton<INavigationService, NavigationService>();



            container.AddCore();
            container.AddThemeManager();
        }

        public void Configure(Container container)
        {
            var applicationSettings = container
                .GetInstance<IApplicationOptions<ApplicationSettings>>().Value;

            LocalizeDictionary.Instance.Culture
                = new CultureInfo(applicationSettings.General.Language);

            container.UseThemeManager()
                .Add("crimson", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Crimson.xaml"));
        }
    }
}
