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
        public DbSet<ReportDTO> Reports { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(@"data source=DataBase\ReportsDataBase.db");
            optionsBuilder.UseSqlite(@"data source=E:\Work\ReportsOrganizer\ReportsOrganizer.DAL\DataBase\ReportsDataBase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        

        
    }
}
