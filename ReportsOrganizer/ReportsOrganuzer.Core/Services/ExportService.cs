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
            var report = await _reportService.GetMonthReportAsync(year, month, CancellationToken.None);
            using (var writer = new StreamWriter(path))
            {
                foreach (var reportForWeek in report)
                {
                    await writer.WriteLineAsync($"Week {reportForWeek.Key}");
                    reportForWeek.Value.ToList().ForEach(async record
                        => await writer.WriteLineAsync(record.ToString()));
                }
            }
        }

        public async Task WriteAll(int year, int month, string path, CancellationToken cancellationToken)
        {
            var report = await _reportService.GetMonthReportAsync(year, month, CancellationToken.None);
            using (var writer = new StreamWriter(path))
            {
                foreach (var reportForWeek in report)
                {
                    await writer.WriteLineAsync($"Week {reportForWeek.Key}");
                    reportForWeek.Value.ToList().ForEach(async record
                        => await writer.WriteLineAsync(record.ToString()));
                }
            }
        }
    }
}
