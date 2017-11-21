using ReportsOrganizer.DAL.Repositories;
using ReportsOrganizer.DI.Extensions;
using SimpleInjector;

namespace ReportsOrganizer.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepository(this Container container)
        {
            container.AddTransient<ApplicationDbContext>();

            container.AddTransient<IReportRepository, ReportRepository>();
            container.AddTransient<IProjectRepository, ProjectRepository>();
        }
    }
}
