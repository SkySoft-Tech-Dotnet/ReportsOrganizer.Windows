using Microsoft.EntityFrameworkCore;
using ReportsOrganizer.DI.Extensions;
using Container = SimpleInjector.Container;

namespace ReportsOrganizer.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessLayer(this Container container)
        {
            container.AddSingleton<IReportRepository, ReportRepository>();
            container.AddSingleton<ApplicationDbContext>();
        }
    }
}
