using System;
using System.Linq;
using System.Web.Http;

using AutoMapper;

using StructureMap;

using TalentManager.Data.Repositories;
using TalentManager.Web.IoC;

namespace TalentManager.Web
{
    public class IocConfig
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        public static void RegisterDependencyResolver(HttpConfiguration config)
        {
            var container = new Container(x =>
            {
                x.Scan(scan =>
                {
                    scan.WithDefaultConventions();

                    AppDomain.CurrentDomain.GetAssemblies()
                                           .Where(a => a.GetName().Name.StartsWith("TalentManager")).ToList()
                                           .ForEach(scan.Assembly);

                    x.For<IMappingEngine>().Use(Mapper.Engine);
                    x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                });
            });
            
            config.DependencyResolver = new StructureMapContainer(container);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}