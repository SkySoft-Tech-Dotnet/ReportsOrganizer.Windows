using ReportsOrganizer.Core.Services;
using SimpleInjector;

namespace ReportsOrganizer.Core.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddConfiguration<T>(this Container container, string fileName)
            where T : class
        {
            container.RegisterSingleton<IConfigurationService<T>>(
                new ConfigurationService<T>(fileName));
        }
    }
}
