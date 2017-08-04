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

    }
    public class ReportsService : IReportsService
    {
        public ReportsService(IReportsRepository reportsRepository)
        {

        }
    }
}
