
using ReportsOrganizer.DAL;
using ReportsOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Services
{
    public interface IReportsService
    {
        void Add(string report);
        Task<Report> GetLastReport();
    }
    public class ReportsService : IReportsService
    {
        IReportsRepository _reportsRepository;

        public ReportsService(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }
        public void Add(string report)
        {            
            _reportsRepository.Add(report);
        }
        public Task<Report> GetLastReport()
        {            
            return _reportsRepository.GetLastReport();
        }
    }
}
