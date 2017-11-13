using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportsOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ReportsOrganizer.DAL.Base;
using ReportsOrganizer.DAL.DTOs;

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

        public async Task Add(string report)
        {
            var reportEentity = new ReportDTO(_reportContext.Reports.Count() + 1,
                DateTime.Now, DateTime.Now, report);

            _reportContext.Reports.Add(reportEentity);
            await _reportContext.SaveChangesAsync();                        
        }

        public async Task<Report> GetLastReport()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReportDTO, Report>();
                //cfg.CreateMap()...
                //cfg.AddProfile()... etc...
            });
            var mapper = config.CreateMapper();
            var lastReport = await _reportContext.Reports.LastOrDefaultAsync();
            return mapper.Map<ReportDTO,Report>(lastReport);            
        }

        //public void Delete(Report entity)
        //{
        //    _reportContext.Reports.Remove(entity);
        //    _reportContext.SaveChanges();
        //}

        //public void Update(Report entity)
        //{
        //    _reportContext.Entry(entity).State = EntityState.Modified;
        //    _reportContext.SaveChanges();

        //}

        public ReportDTO FindById(int Id)
        {
            var result = (from r in _reportContext.Reports where r.Id == Id select r).FirstOrDefault();
            return result;
        }

    }
}
