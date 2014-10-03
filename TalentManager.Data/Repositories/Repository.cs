using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using TalentManager.Domain;

namespace TalentManager.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IIdentifiable
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly MyContext context;

        //// ----------------------------------------------------------------------------------------------------------

        public Repository(IUnitOfWork unitOfWork)
        {
            this.context = unitOfWork.Context as MyContext;
        }
 
        //// ----------------------------------------------------------------------------------------------------------

        public IQueryable<T> All
        {
            get
            {
                return this.context.Set<T>();
            }
        }

        //// ----------------------------------------------------------------------------------------------------------

        public IQueryable<T> AllEager(params Expression<Func<T, object>>[] includes)
        {
            var query = this.All;

            foreach (var include in includes)
                query.Include(include);

            return query;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public T Find(int id)
        {
            return this.context.Set<T>().Find(id);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void Insert(T item)
        {
            this.context.Entry(item).State = EntityState.Added;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void Update(T item)
        {
            this.context.Set<T>().Attach(item);
            this.context.Entry(item).State = EntityState.Modified;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void Delete(int id)
        {
            var item = this.Find(id);
            this.context.Set<T>().Remove(item);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            if (this.context != null)
                this.context.Dispose();
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
