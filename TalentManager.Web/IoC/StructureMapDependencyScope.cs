using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

using StructureMap;

namespace TalentManager.Web.IoC
{
    public class StructureMapDependencyScope : IDependencyScope
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        protected readonly IContainer container;

        //// ----------------------------------------------------------------------------------------------------------

        public StructureMapDependencyScope(IContainer container)
        {
            this.container = container;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public object GetService(Type serviceType)
        {
            var isConcrete = !serviceType.IsAbstract && !serviceType.IsInterface;

            return isConcrete ? this.container.GetInstance(serviceType) : this.container.TryGetInstance(serviceType);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.GetAllInstances<object>().Where(s => s.GetType() == serviceType);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            if (this.container != null)
                this.container.Dispose();
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}