using AutoMapper;

using TalentManager.Domain;
using TalentManager.Web.DTOs;

namespace TalentManager.Web
{
    public class DtoMapperConfig
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        public static void CreateMaps()
        {
            Mapper.CreateMap<EmployeeDto, Employee>();
            Mapper.CreateMap<Employee, EmployeeDto>();
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}