
using ReportsOrganizer.DAL;
using ReportsOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReportsOrganizer.Core.Services
{
    public interface IReportService
    {
        void Add(string description, CancellationToken cancellationToken);
        Task<Report> GetLastReport(CancellationToken cancellationToken);
    }
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportsRepository;

        public ReportService(IReportRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }
        public void Add(string description, CancellationToken cancellationToken)
        {
            var newReport = new Report{ Description = description };
            _reportsRepository.AddAsync(newReport, cancellationToken);
        }

        public Task<Report> GetLastReport(CancellationToken cancellationToken)
        {
            return _reportsRepository.LastReport.FirstAsync(cancellationToken);
        }
    }
}
