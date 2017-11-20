
using System.Collections.Generic;
using ReportsOrganizer.DAL;
using ReportsOrganizer.Models;
using System.Threading;
using System.Threading.Tasks;
using ReportsOrganizer.Core.Abstractions;
using ReportsOrganizer.DAL.Abstractions;
using ReportsOrganizer.DAL.Repositories;
using System;
using System.Data.Entity;
using System.Globalization;

namespace ReportsOrganizer.Core.Services
{
    public interface IReportService : IBaseService<Report>
    {
        Task<IEnumerable<Report>> GetWeekReport(int year, int month, int week, CancellationToken cancellationToken);
        int GetWeeksOfMonth(int year, int month);
        Task<Report> GetLastReport(CancellationToken cancellationToken);
    }
    internal class ReportService : BaseService<Report>, IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository) : base(reportRepository)
            => _reportRepository = reportRepository;

        public async Task<IEnumerable<Report>> GetWeekReport(int year, int month, int week, CancellationToken cancellationToken)
        {
            var begin = new DateTime(year, month, 1);
            var beginWeekDay = (int)begin.DayOfWeek;

            var beginDay = (week - 1) * 7 - beginWeekDay + 1;
            var beginDate = new DateTime(year, month, beginDay > 0 ? beginDay : 1);

            var endDay = week * 7 - beginWeekDay;
            var maxDays = DateTime.DaysInMonth(year, month);
            var endDate = new DateTime(year, month, endDay > maxDays ? maxDays : endDay);

            return await _reportRepository.FindReport(beginDate, endDate)
                .Include(table => table.Project)
                .ToListAsync(cancellationToken);
        }

        public int GetWeeksOfMonth(int year, int month)
        {
            var culture = CultureInfo.CurrentCulture;

            var beginDay = new DateTime(year, month, 1);
            var endDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            var beginWeek = culture.Calendar.GetWeekOfYear(beginDay, culture.DateTimeFormat.CalendarWeekRule, DayOfWeek.Sunday);
            var endWeek = culture.Calendar.GetWeekOfYear(endDay, culture.DateTimeFormat.CalendarWeekRule, DayOfWeek.Sunday);

            return endWeek - beginWeek + 1;
        }

        public Task<Report> GetLastReport(CancellationToken cancellationToken)
        {
            return _reportRepository.LastReport.FirstAsync(cancellationToken);
        }
    }
}
