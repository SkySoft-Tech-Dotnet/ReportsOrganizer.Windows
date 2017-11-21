//using Microsoft.EntityFrameworkCore;
using ReportsOrganizer.Models;

namespace ReportsOrganizer.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
        public DbSet<Project> Projects { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"data source=Reports.db");
        }
    }
}
