using System.Reflection;
using WPFLocalizeExtension.Extensions;

namespace ReportsOrganizer.UI.Helpers
{
    internal static class LocalizationHelper
    {
        public static T GetLocalizedValue<T>(string path)
        {
            var ns = Assembly.GetCallingAssembly().GetName().Name;
            return LocExtension.GetLocalizedValue<T>($"{ns}:{path}");
        }

        public static string GetLocalizedValue(string path)
        {
            return GetLocalizedValue<string>(path);
        }
    }
}
