using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOrganizer.DAL
{
    public interface IReportsRepository : IRepository<ReportDTO>
    {

    }

    public class ReportsRepository : IReportsRepository
    {
        ReportDBContext _reportContext;

        public ReportsRepository()
        {
            _reportContext = new ReportDBContext();

        }
        public IEnumerable<ReportDTO> List
        {
            get
            {
                return _reportContext.Reports;
            }

        }

        public void Add(ReportDTO entity)
        {
            _reportContext.Reports.Add(entity);
            _reportContext.SaveChanges();
        }

        public void Delete(ReportDTO entity)
        {
            _reportContext.Reports.Remove(entity);
            _reportContext.SaveChanges();
        }

        public void Update(ReportDTO entity)
        {
            _reportContext.Entry(entity).State = EntityState.Modified;
            _reportContext.SaveChanges();

        }

        public ReportDTO FindById(int Id)
        {
            var result = (from r in _reportContext.Reports where r.Id == Id select r).FirstOrDefault();
            return result;
        }

    }
}
