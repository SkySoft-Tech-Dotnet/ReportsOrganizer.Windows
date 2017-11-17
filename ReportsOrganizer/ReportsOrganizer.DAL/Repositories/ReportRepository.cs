using ReportsOrganizer.DAL.Abstractions;
using ReportsOrganizer.Models;
using System.Linq;

namespace ReportsOrganizer.DAL
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        IQueryable<Report> LastReport { get; }
    }

    internal class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly ApplicationDbContext _applicationContext;

        public ReportRepository(ApplicationDbContext applicationContext) : base(applicationContext)
            => _applicationContext = applicationContext;

        public IQueryable<Report> LastReport
            => _applicationContext.Reports
                .OrderByDescending(property => property.Id).Take(1);
    }
}
