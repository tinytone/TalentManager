using System;
using System.Collections.Generic;
using System.Linq;

using TalentManager.Domain;

namespace TalentManager.Data.Repositories
{
    /*
    /// <summary>
    ///  Repository Pattern: Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects.  
    /// 
    /// http://martinfowler.com/eaaCatalog/repository.html
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly Context context;

        //// ----------------------------------------------------------------------------------------------------------
		 
        private bool disposed;

        //// ----------------------------------------------------------------------------------------------------------

        public EmployeeRepository()
        {
            this.context = new Context();
            this.disposed = false;
        }

        //// ----------------------------------------------------------------------------------------------------------
		
        public IEnumerable<Employee> GetByDepartment(int departmentId)
        {
            return context.Employees.Where(e => e.DepartmentId == departmentId);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public Employee Get(int id)
        {
            return context.Employees.FirstOrDefault(e => e.Id == id);
        }

        //// ----------------------------------------------------------------------------------------------------------

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.context != null)
                        this.context.Dispose();
                }
            }

            this.disposed = true;
        }
        //// ----------------------------------------------------------------------------------------------------------
		 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
     */
}
