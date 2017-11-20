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
using WPFLocalizeExtension.Engine;

namespace ReportsOrganizer.UI
{
    public class Startup
    {
        public void ConfigureServices(Container container)
        {
            container.AddConfiguration<ApplicationSettings>(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));

            container.AddTransient<GeneralViewModel>();
            container.AddTransient<ManageProjectsViewModel>();
            container.AddTransient<NotificationViewModel>();
            container.AddTransient<PersonalizationViewModel>();

            container.AddTransient<MainWindowViewModel>();
            container.AddTransient<ManageProjectWindowViewModel>();
            container.AddTransient<NotificationWindowViewModel>();

            container.AddCore();
            container.AddThemeManager();

            
            container.AddSingleton<INotificationService, NotificationService>();
            container.AddSingleton<INavigationService, NavigationService>();
        }

        public void Configure(Container container)
        {
            var applicationSettings = container
                .GetInstance<IApplicationOptions<ApplicationSettings>>().Value;

            LocalizeDictionary.Instance.Culture
                = new CultureInfo(applicationSettings.General.Language);

            container.UseThemeManager()
                .AddTheme("Amber", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Amber.xaml"))
                .AddTheme("Cobalt", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Cobalt.xaml"))
                .AddTheme("Crimson", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Crimson.xaml"))
                .AddTheme("Cyan", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Cyan.xaml"))
                .AddTheme("Emerald", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Emerald.xaml"))
                .AddTheme("Green", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Green.xaml"))
                .AddTheme("Indigo", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Indigo.xaml"))
                .AddTheme("Magenta", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Magenta.xaml"))
                .AddTheme("Orange", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Orange.xaml"))
                .AddTheme("Purple", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Purple.xaml"))
                .AddTheme("Teal", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Teal.xaml"));
        }
    }
}
