using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using TalentManager.Data.Configuration;
using TalentManager.Domain;

namespace TalentManager.Data
{
    /*
    public class Context : DbContext, IContext
    {
        //// ----------------------------------------------------------------------------------------------------------

        public IDbSet<Employee> Employees { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public IDbSet<Department> Departments { get; set; }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public Context() : base("DefaultConnectionString")
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
     * */
}
