using ReportsOrganizer.Models;
using System.Data.Entity;

namespace ReportsOrganizer.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
        public DbSet<Project> Projects { get; set; }

        public ApplicationDbContext() : base("ReportConnection")
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
