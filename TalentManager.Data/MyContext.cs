using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using TalentManager.Data.Configuration;

namespace TalentManager.Data
{
    public class MyContext : DbContext, IMyContext
    {
        //// ----------------------------------------------------------------------------------------------------------

        static MyContext()
        {
            Database.SetInitializer<MyContext>(null);    
        }

        //// ----------------------------------------------------------------------------------------------------------

        public MyContext() : base("DefaultConnectionString")
        {
        }

        //// ----------------------------------------------------------------------------------------------------------

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new EmployeeConfiguration())
                                       .Add(new DepartmentConfiguration());
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
