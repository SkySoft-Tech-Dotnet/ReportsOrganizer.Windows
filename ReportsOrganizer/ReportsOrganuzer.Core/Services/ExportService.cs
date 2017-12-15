using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReportsOrganizer.Models;

namespace ReportsOrganizer.Core.Services
{
    public interface IExportService
    {
        Task WriteMonthReport(int year, int month, string path, CancellationToken cancellationToken);

        Task WriteAll(string path, CancellationToken cancellationToken);
    }

    internal class ExportService : IExportService
    {
        private readonly IReportService _reportService;

        public ExportService(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task WriteMonthReport(int year, int month, string path, CancellationToken cancellationToken)
        {
            var reports = await _reportService.FindMonthReportsAsync(year, month, CancellationToken.None);
            var reportsStr = new List<string>();

            foreach (var reportForWeek in reports)
            {
                //if (!reportForWeek.Value.Any()) continue;

                reportsStr.Add($"Week {reportForWeek.Key}");
                reportForWeek.Value.ToList().ForEach(record
                    => reportsStr.Add(record.ToString()));
            }

            File.WriteAllLines(path, reportsStr, Encoding.ASCII);
        }

        public async Task WriteAll(string path, CancellationToken cancellationToken)
        {
            var reports = await _reportService.FindReportsAsync(cancellationToken);

            File.WriteAllLines(path, reports.Select(e => e.ToString()), Encoding.ASCII);
        }
    }
}
