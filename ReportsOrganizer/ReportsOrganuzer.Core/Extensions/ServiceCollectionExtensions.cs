using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DAL;
using SimpleInjector;
using System.Threading;

namespace ReportsOrganizer.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTransient<TService, TImplementation>(this Container container)
            where TService : class
            where TImplementation : class, TService
        {
            container.Register(typeof(TService), typeof(TImplementation), Lifestyle.Transient);
        }

        public static void AddScoped<TService, TImplementation>(this Container container)
            where TService : class
            where TImplementation : class, TService
        {
            container.Register(typeof(TService), typeof(TImplementation), Lifestyle.Scoped);
        }

        public static void AddSingleton<TService, TImplementation>(this Container container)
            where TService : class
            where TImplementation : class, TService
        {
            container.RegisterSingleton<TService, TImplementation>();
        }

        public static void AddSingleton<TService, TImplementation>(this Container container, TImplementation implementation)
            where TService : class
            where TImplementation : class, TService
        {
            container.RegisterSingleton<TService>(implementation);
        }

        public static void AddConfiguration<T>(this Container container, string path)
            where T : class
        {
            var applicationOptions = new ApplicationOptions<T>(path);
            applicationOptions.LoadAsync(default(CancellationToken)).Wait();

            container.RegisterSingleton<IApplicationOptions<T>>(applicationOptions);
        }

        public static void AddCore(this Container container)
        {
            container.AddSingleton<IReportsRepository, ReportsRepository>();
            container.AddSingleton<IReportsService, ReportsService>();
        }
    }
}
