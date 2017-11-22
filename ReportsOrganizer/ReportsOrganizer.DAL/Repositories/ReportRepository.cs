using ReportsOrganizer.DAL.Abstractions;
using ReportsOrganizer.Models;
using System;
using System.Linq;

namespace ReportsOrganizer.DAL.Repositories
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        IQueryable<Report> FindReport(DateTime startDate, DateTime endDate);
        IQueryable<Report> FindReports();
    }

    internal class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReportRepository(ApplicationDbContext dbContext) : base(dbContext)
            => _dbContext = dbContext;

        public IQueryable<Report> FindReport(DateTime startDate, DateTime endDate)
            => _dbContext.Reports.Where(property
                => property.Created >= startDate && property.Created <= endDate);

        public IQueryable<Report> FindReports()
            => _dbContext.Reports.OrderByDescending(property => property.Created);
    }
}
