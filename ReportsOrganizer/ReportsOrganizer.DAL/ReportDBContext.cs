using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOrganizer.DAL
{
    public class ReportDBContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }

        private static ReportDBContext instance;
        private ReportDBContext() { }

        public static ReportDBContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReportDBContext();
                }
                return instance;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"data source=DataBase\ReportsDataBase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        

        
    }
}
