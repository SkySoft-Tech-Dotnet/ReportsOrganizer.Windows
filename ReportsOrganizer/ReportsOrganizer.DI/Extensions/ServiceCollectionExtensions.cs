using SimpleInjector;

namespace ReportsOrganizer.DI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTransient<TService, TImplementation>(this Container container)
            where TService : class
            where TImplementation : class, TService
        {
            container.Register(typeof(TService), typeof(TImplementation), Lifestyle.Transient);
        }

        public static void AddTransient<TService>(this Container container)
            where TService : class
        {
            container.Register(typeof(TService));
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

        public static void AddSingleton<TService, TImplementation>(this Container container,
            TImplementation implementation)
            where TService : class
            where TImplementation : class, TService
        {
            container.RegisterSingleton<TService>(implementation);
        }
    }
}
