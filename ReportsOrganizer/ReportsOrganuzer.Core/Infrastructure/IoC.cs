using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DAL;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Infrastructure
{
    public class IoC
    {
        private static Container _container;

        private IoC() { }

        public static Container Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new Container();
                    _container.Register<IReportsRepository, ReportsRepository>(Lifestyle.Singleton);
                    _container.Register<IReportsService, ReportsService>(Lifestyle.Singleton);
                }
                return _container;
            }
        }        
    }
}
