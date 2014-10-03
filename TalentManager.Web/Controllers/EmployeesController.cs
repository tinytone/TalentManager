using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using AutoMapper;

using TalentManager.Data;
using TalentManager.Data.Repositories;
using TalentManager.Domain;
using TalentManager.Web.DTOs;

namespace TalentManager.Web.Controllers
{
    public class EmployeesController : ApiController
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Employee> repository;
        private readonly IMappingEngine mapper;
        
        //// ----------------------------------------------------------------------------------------------------------

        public EmployeesController(IUnitOfWork unitOfWork, IRepository<Employee> repository, IMappingEngine mapper)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public HttpResponseMessage Get(int id)
        {
            var employee = this.repository.Find(id);

            if (employee == null)
            {
                var response = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee not found");
                throw new HttpResponseException(response);
            }

            var employeeDto = this.mapper.Map<Employee, EmployeeDto>(employee);
            return Request.CreateResponse(HttpStatusCode.OK, employeeDto);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public HttpResponseMessage GetByDepartmentId(int departmentId)
        {
            var employees = this.repository.All.Where(e => e.DepartmentId == departmentId).ToList();

            if (employees.Any())
            {
                var employeeDTOs = this.mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

                return Request.CreateResponse(HttpStatusCode.OK, employeeDTOs);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public HttpResponseMessage Post([FromBody]EmployeeDto employeeDto)
        {
            var employee = this.mapper.Map<EmployeeDto, Employee>(employeeDto);

            this.repository.Insert(employee);
            this.unitOfWork.Save();

            var response = Request.CreateResponse(HttpStatusCode.Created, employee);

            var uri = Url.Link("DefaultApi", new { id = employee.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void Put(int id, Employee employee)
        {
            this.repository.Update(employee);
            this.unitOfWork.Save();
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void Delete(int id)
        {
            this.repository.Delete(id);
            this.unitOfWork.Save();
        }

        //// ----------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            if (this.repository != null)
                this.repository.Dispose();

            if (this.unitOfWork != null)
                this.unitOfWork.Dispose();

            base.Dispose(disposing);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
