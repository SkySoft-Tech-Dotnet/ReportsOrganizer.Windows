using ReportsOrganizer.DAL;
using ReportsOrganizer.Models;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using ReportsOrganizer.Core.Abstractions;
using ReportsOrganizer.DAL.Abstractions;
using ReportsOrganizer.DAL.Repositories;

namespace ReportsOrganizer.Core.Services
{
    public interface IReportService : IBaseService<Report>
    {
        void Add(string description, CancellationToken cancellationToken);
        Task<Report> GetLastReport(CancellationToken cancellationToken);
    }
    internal class ReportService : BaseService<Report>, IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository) : base(reportRepository)
            => _reportRepository = reportRepository;

        public void Add(string description, CancellationToken cancellationToken)
        {
            var newReport = new Report { Description = description };
            _reportRepository.AddAsync(newReport, cancellationToken);
        }

        public Task<Report> GetLastReport(CancellationToken cancellationToken)
        {
            return _reportRepository.LastReport.FirstAsync(cancellationToken);
        }
    }
}
