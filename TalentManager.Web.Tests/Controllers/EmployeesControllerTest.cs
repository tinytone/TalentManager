using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

using AutoMapper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rhino.Mocks;

using TalentManager.Data;
using TalentManager.Data.Repositories;
using TalentManager.Domain;
using TalentManager.Web.Controllers;
using TalentManager.Web.DTOs;
using TalentManager.Web.Tests.Extensions;

namespace TalentManager.Web.Tests.Controllers
{
    [TestClass]
    public class EmployeesControllerTest
    {
        private MockRepository mocks;

        //// ----------------------------------------------------------------------------------------------------------

        [TestInitialize]
        public void TestInitialize()
        {
            this.mocks = new MockRepository();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        [TestCleanup]
        public void TestCleanup()
        {
            this.mocks.VerifyAll();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        [TestMethod]
        public void Get_ValidId_ExpectValidEmployee()
        {
            // Arrange
            const int Id = 12345;

            var employee = new Employee { Id = Id, FirstName = "Tony", LastName = "Harding" };

            var repository = this.mocks.StrictMock<IRepository<Employee>>();
            Expect.Call(repository.Find(Id)).Return(employee);

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            Mapper.CreateMap<Employee, EmployeeDto>();

            this.mocks.ReplayAll();

            var controller = new EmployeesController(unitOfWork, repository, Mapper.Engine);
            controller.EnsureNotNull();

            // Act
            var response = controller.Get(Id);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsInstanceOfType(response.Content, typeof(ObjectContent<EmployeeDto>));
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var content = response.Content as ObjectContent<EmployeeDto>;
            var result = content.Value as EmployeeDto;

            Assert.AreEqual(result.Id, employee.Id);
            Assert.AreEqual(result.FirstName, employee.FirstName);
            Assert.AreEqual(result.LastName, employee.LastName);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void Get_InvalidId_Expect404NotFoundStatusCode()
        {
            // Arrange
            const int Id = 12345;

            Employee employee = null;

            var repository = this.mocks.StrictMock<IRepository<Employee>>();
            Expect.Call(repository.Find(Id)).Return(employee);

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            Mapper.CreateMap<Employee, EmployeeDto>();

            this.mocks.ReplayAll();

            var controller = new EmployeesController(unitOfWork, repository, Mapper.Engine);
            controller.EnsureNotNull();

            // Act
            try
            {
                controller.Get(Id);
            }
            catch (HttpResponseException ex)
            {
                // Assert
                Assert.AreEqual(ex.Response.StatusCode, HttpStatusCode.NotFound);

                throw;
            }
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Post_ValidEmployee_Expect201StatusCodeAndLinkForPost()
        {
            // Arrange
            const int Id = 12345;
            const string RequestUri = "http://localhost:8086/api/employees/";

            var employeeDto = new EmployeeDto { Id = 0, FirstName = "Tony", LastName = "Harding", DepartmentId = 5 };
            var employee = new Employee
                               {
                                   Id = 12345, 
                                   FirstName = employeeDto.FirstName, 
                                   LastName = employeeDto.LastName, 
                                   DepartmentId = employeeDto.DepartmentId,
                               };

            var mapper = this.mocks.StrictMock<IMappingEngine>();
            Expect.Call(mapper.Map<EmployeeDto, Employee>(employeeDto)).Return(employee);

            var repository = this.mocks.StrictMock<IRepository<Employee>>();
            Expect.Call(() => repository.Insert(employee));

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            Expect.Call(unitOfWork.Save()).Return(1);

            this.mocks.ReplayAll();

            var controller = new EmployeesController(unitOfWork, repository, mapper);
            controller.SetRequest("employees", HttpMethod.Post, RequestUri);

            // Act
            var response = controller.Post(employeeDto);

            // Assert
            var uriForNewEmployee = new Uri(new Uri(RequestUri), Id.ToString());

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.AreEqual(response.Headers.Location, uriForNewEmployee);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
