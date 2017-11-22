using ReportsOrganizer.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //var model = modelBuilder.Build(Database.Connection);
        //    //ISqlGenerator sqlGenerator = new SqliteSqlGenerator();
        //    //string sql = sqlGenerator.Generate(model.StoreModel);

        //    //base.OnModelCreating(modelBuilder);

        //    //var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DbContext>(modelBuilder);
        //    //Database.SetInitializer(sqliteConnectionInitializer);

        //    Database.SetInitializer<DbContext>(null);
        //    modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        //}
    }
}
