using MahApps.Metro;
using ReportsOrganizer.Core.Extensions;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DI.Extensions;
using ReportsOrganizer.UI.Models;
using ReportsOrganizer.UI.Services;
using ReportsOrganizer.UI.ViewModels.Settings;
using ReportsOrganizer.UI.ViewModels.Windows;
using SimpleInjector;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using ReportsOrganizer.Models;
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

            container.AddSingleton<MainWindowViewModel>();
            container.AddSingleton<NotificationWindowViewModel>();

            container.AddCore();

            ThemeManager.AddAccent("Amber", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Amber.xaml"));
            ThemeManager.AddAccent("Cobalt", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Cobalt.xaml"));
            ThemeManager.AddAccent("Crimson", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Crimson.xaml"));
            ThemeManager.AddAccent("Cyan", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Cyan.xaml"));
            ThemeManager.AddAccent("Emerald", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Emerald.xaml"));
            ThemeManager.AddAccent("Green", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Green.xaml"));
            ThemeManager.AddAccent("Indigo", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Indigo.xaml"));
            ThemeManager.AddAccent("Magenta", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Magenta.xaml"));
            ThemeManager.AddAccent("Orange", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Orange.xaml"));
            ThemeManager.AddAccent("Purple", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Purple.xaml"));
            ThemeManager.AddAccent("Teal", new Uri("pack://application:,,,/ReportsOrganizer.UI;component/Themes/Teal.xaml"));


            container.AddSingleton<INotificationService, NotificationService>();
            container.AddSingleton<INavigationService, NavigationService>();
        }

        public void Configure(IApplicationOptions<ApplicationSettings> applicationSettings, IProjectService projectService)
        {
            LocalizeDictionary.Instance.Culture = new CultureInfo(applicationSettings.Value.General.Language);

            ThemeManager.ChangeAppStyle(
                Application.Current,
                ThemeManager.GetAccent(applicationSettings.Value.Personalization.Theme),
                ThemeManager.DetectAppStyle(Application.Current).Item1);

            //TEST PROJECT SERVICE
            //projectService.AddAsync(new Project
            //{
            //    ShortName = "UCCC",
            //    FullName = "LongName",
            //    IsActive = true
            //}, CancellationToken.None).Wait();

            //var a = projectService.ToListAsync(CancellationToken.None).Result;
            //projectService.DeleteAsync(a.First(), CancellationToken.None);
        }
    }
}
