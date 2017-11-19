using System.Reflection;
using WPFLocalizeExtension.Extensions;

namespace ReportsOrganizer.Localization.Helpers
{
    public static class LocalizationHelper
    {
        public static T GetLocalizedValue<T>(string path)
        {
            return LocExtension.GetLocalizedValue<T>(
                $"{Assembly.GetExecutingAssembly().GetName().Name}:{path}");
        }

        public static string GetLocalizedValue(string path)
        {
            return GetLocalizedValue<string>(path);
        }
    }
}
