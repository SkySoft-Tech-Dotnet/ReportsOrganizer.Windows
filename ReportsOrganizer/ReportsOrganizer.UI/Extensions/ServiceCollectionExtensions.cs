using MahApps.Metro;
using ReportsOrganizer.DI.Extensions;
using SimpleInjector;
using System;
using System.Windows;

namespace ReportsOrganizer.UI.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddThemeManager(this Container container)
        {
            container.AddSingleton<ApplicationTheme>();
        }

        public static ApplicationTheme UseThemeManager(this Container container)
        {
            return container.GetInstance<ApplicationTheme>();
        }

        public static void UseTheme(this Container container, string name)
        {
            var appTheme = ThemeManager.DetectAppStyle(Application.Current);
            var appAccent = ThemeManager.GetAccent(name);
            if (appAccent != null)
            {
                ThemeManager.ChangeAppStyle(Application.Current, appAccent, appTheme.Item1);
            }
        }
    }

    public class ApplicationTheme
    {
        public ApplicationTheme AddTheme(string name, Uri uri)
        {
            if(ThemeManager.AddAccent(name, uri))
            {
                throw new Exception("Theme does not exists or can be added!");
            }
            return this;
        }
    }
}
