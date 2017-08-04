using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOrganizer.DAL
{

    public class ReportRepository : IRepository<Report>
    {
        ReportDBContext _reportContext;

        public ReportRepository()
        {
            _reportContext = ReportDBContext.Instance;

        }
        public IEnumerable<Report> List
        {
            get
            {
                return _reportContext.Reports;
            }

        }

        public void Add(Report entity)
        {
            _reportContext.Reports.Add(entity);
            _reportContext.SaveChanges();
        }

        public void Delete(Report entity)
        {
            _reportContext.Reports.Remove(entity);
            _reportContext.SaveChanges();
        }

        public void Update(Report entity)
        {
            _reportContext.Entry(entity).State = EntityState.Modified;
            _reportContext.SaveChanges();

        }

        public Report FindById(int Id)
        {
            var result = (from r in _reportContext.Reports where r.Id == Id select r).FirstOrDefault();
            return result;
        }

    }
}
