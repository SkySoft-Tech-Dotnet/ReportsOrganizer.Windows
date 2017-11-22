using ReportsOrganizer.Models;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace ReportsOrganizer.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
        public DbSet<Project> Projects { get; set; }

        public ApplicationDbContext() : base("ReportConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
