using ReportsOrganizer.Core.Services;
using SimpleInjector;
using System.Threading;

namespace ReportsOrganizer.Core.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddConfiguration2<T>(this Container container, string fileName)
            where T : class
        {
            container.RegisterSingleton<IConfigurationService<T>>(
                new ConfigurationService<T>(fileName));
        }

        public static void AddConfiguration<T>(this Container container, string path)
            where T : class
        {
            var applicationOptions = new ApplicationOptions<T>(path);
            applicationOptions.LoadAsync(default(CancellationToken)).Wait();

            container.RegisterSingleton<IApplicationOptions<T>>(applicationOptions);
        }
    }
}
