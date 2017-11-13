using SimpleInjector;

namespace ReportsOrganizer.DI.Providers
{
    public class ServiceCollectionProvider
    {
        private static Container _container;

        private ServiceCollectionProvider() { }

        public static Container Container =>
            _container ?? (_container = new Container());
    }
}
