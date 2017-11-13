using ReportsOrganizer.DI.Extensions;
using ReportsOrganizer.UI.Models;
using SimpleInjector;
using System;
using System.Collections.Generic;

namespace ReportsOrganizer.UI.Extensions
{
    public static class ThemeServiceCollectionExtensions
    {
        public static void AddThemeManager(this Container container)
        {
            container.AddSingleton<ApplicationTheme>();
        }

        public static Dictionary<string, Uri> UseThemeManager(this Container container)
        {
            return container.GetInstance<ApplicationTheme>().Themes;
        }
    }
}
