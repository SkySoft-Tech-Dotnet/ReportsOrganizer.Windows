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

namespace ReportsOrganizer.DAL
{
    public interface IReportRepository : IBaseRepository<Report>
    {

    }

    internal class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly ApplicationDbContext _applicationContext;

        public ReportRepository(ApplicationDbContext applicationContext):base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
