using System.Web.Http.Dependencies;

using StructureMap;

namespace TalentManager.Web.IoC
{
    public class StructureMapContainer : StructureMapDependencyScope, IDependencyResolver
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        public StructureMapContainer(IContainer container)
            : base(container)
        {
        }

        //// ----------------------------------------------------------------------------------------------------------

        public IDependencyScope BeginScope()
        {
            return new StructureMapDependencyScope(container.GetNestedContainer());
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}