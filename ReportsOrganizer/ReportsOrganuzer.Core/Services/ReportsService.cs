using ReportsOrganizer.DAL;
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
    }
}
