using ReportsOrganizer.Models;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace ReportsOrganizer.DAL
{
    public class ApplicationDbInitializer : SqliteCreateDatabaseIfNotExists<ApplicationDbContext>
    {
        public ApplicationDbInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Set<Project>().Add(new Project
            {
                FullName = "Investigation",
                ShortName = "INV",
                IsActive = true
            });

            context.Set<Project>().Add(new Project
            {
                FullName = "Other",
                ShortName = "OTHER",
                IsActive = true
            });
        }
    }
}
