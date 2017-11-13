using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DAL;
using ReportsOrganizer.DI.Extensions;
using SimpleInjector;
using System.Threading;
using ReportsOrganizer.DAL.Extensions;

namespace ReportsOrganizer.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfiguration<T>(this Container container, string path)
            where T : class
        {
            var applicationOptions = new ApplicationOptions<T>(path);
            applicationOptions.LoadAsync(default(CancellationToken)).Wait();

            container.RegisterSingleton<IApplicationOptions<T>>(applicationOptions);
        }

        public static void AddCore(this Container container)
        {
            //container.AddSingleton<IReportRepository, ReportRepository>();
            container.AddSingleton<IReportService, ReportService>();
            container.AddDataAccessLayer();
        }
    }
}
