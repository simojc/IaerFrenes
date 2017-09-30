using System.Data.Entity;
using EntityFramework.CodeFirst.Migrations;

namespace EntityFramework.CodeFirst
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            //Important performance code
            Configuration.AutoDetectChangesEnabled = false; 
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        static ApplicationDbContext()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, ApplicationDbInitializer>());

            // put this in the project startup 
            Database.SetInitializer<ApplicationDbContext>(new NullDatabaseInitializer<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<FreneValue.Models.COD_DOM> COD_DOM { get; set; }

        // public DbSet<MyClass> Educations { get; set; } //Example for creating db table 
    }
}
