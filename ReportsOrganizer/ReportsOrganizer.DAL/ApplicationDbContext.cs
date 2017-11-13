using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsOrganizer.Models;

namespace ReportsOrganizer.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(@"data source=DataBase\ReportsDataBase.db");
            optionsBuilder.UseSqlite(@"data source=E:\Work\ReportsOrganizer\ReportsOrganizer.DAL\DataBase\ReportDatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        

        
    }
}
