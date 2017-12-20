using ReportsOrganizer.Core.Abstractions;
using ReportsOrganizer.DAL.Repositories;
using ReportsOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Services
{
    public interface IReportService : IBaseService<Report>
    {
        Task<IEnumerable<Report>> FindReportsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Report>> FindReportsAsync(int year, int month, int week, CancellationToken cancellationToken);
        Task<Report> FindLastReportAsync(CancellationToken cancellationToken);

        Task<Dictionary<int, IEnumerable<Report>>> FindMonthReportsAsync(int year, int month, CancellationToken cancellationToken);
        int GetWeeksOfMonth(int year, int month);
    }
    internal class ReportService : BaseService<Report>, IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository) : base(reportRepository)
            => _reportRepository = reportRepository;

        public async Task<IEnumerable<Report>> FindReportsAsync(CancellationToken cancellationToken)
        {
            var reports = await _reportRepository.FindReports()
                .Include(table => table.Project)
                .ToListAsync(cancellationToken);

            return reports;
        }

        public async Task<IEnumerable<Report>> FindReportsAsync(int year, int month, int week,
            CancellationToken cancellationToken)
        {
            var begin = new DateTime(year, month, 1);
            var beginWeekDay = (int)begin.DayOfWeek;

            var beginDay = (week - 1) * 7 - beginWeekDay + 1;
            var beginDate = new DateTime(year, month, beginDay > 0 ? beginDay : 1);

            var endDay = week * 7 - beginWeekDay;
            var maxDays = DateTime.DaysInMonth(year, month);
            var endDate = new DateTime(year, month, endDay > maxDays ? maxDays : endDay);

            return await _reportRepository.FindReports(beginDate, endDate)
                .Include(table => table.Project)
                .ToListAsync(cancellationToken);
        }

        public Task<Report> FindLastReportAsync(CancellationToken cancellationToken)
        {
            return _reportRepository.FindReports()
                .Include(table => table.Project)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Dictionary<int, IEnumerable<Report>>> FindMonthReportsAsync(int year, int month, CancellationToken cancellationToken)
        {
            var numberOfWeeks = GetWeeksOfMonth(year, month);

            var index = 0;
            var dict = new Dictionary<int, IEnumerable<Report>>();

            while (index++ < numberOfWeeks)
            {
                dict.Add(index, await FindReportsAsync(year, month, index, cancellationToken));
            }

            return dict;
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
    }
}
