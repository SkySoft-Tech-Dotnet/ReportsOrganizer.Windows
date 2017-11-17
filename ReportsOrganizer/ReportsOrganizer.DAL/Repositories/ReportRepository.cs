using System.Linq;
using ReportsOrganizer.DAL.Abstractions;
using ReportsOrganizer.Models;

namespace ReportsOrganizer.DAL.Repositories
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        IQueryable<Report> LastReport { get; }
    }

    internal class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly ApplicationDbContext _applicationContext;

        public ReportRepository(ApplicationDbContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IQueryable<Report> LastReport
            => _applicationContext.Reports
                .OrderByDescending(property => property.EndDate).Take(1);
    }
}
