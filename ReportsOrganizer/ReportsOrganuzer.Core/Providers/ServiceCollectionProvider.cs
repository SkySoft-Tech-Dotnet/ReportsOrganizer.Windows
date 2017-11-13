using SimpleInjector;

namespace ReportsOrganizer.Core.Providers
{
    public class ServiceCollectionProvider
    {
        private static Container _container;

        private ServiceCollectionProvider() { }

        public static Container Container
        {
            get
            {
                return _container ?? (_container = new Container());
            }
        }
    }
}
