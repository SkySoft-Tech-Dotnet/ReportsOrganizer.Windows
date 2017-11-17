using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DAL.Extensions;
using ReportsOrganizer.DI.Extensions;
using SimpleInjector;
using System.Threading;

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
            container.AddTransient<IApplicationManage, ApplicationManage>();
            
            container.AddSingleton<IReportService, ReportService>();
            container.AddSingleton<IProjectService, ProjectService>();
            container.AddRepository();
        }
    }
}
