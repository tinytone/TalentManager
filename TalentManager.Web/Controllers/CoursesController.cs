using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TalentManager.Web.Controllers
{
    public class CoursesController : ApiController
    {
        //// ----------------------------------------------------------------------------------------------------------

        private static List<Course> courses = InitCourses();

        //// ----------------------------------------------------------------------------------------------------------
		 
        private static List<Course> InitCourses()
        {
            return new List<Course>
                       {
                           new Course { Id = 0, Title = "Web Api" },
                           new Course { Id = 1, Title = "ASP MVC" },
                       };
        }

        //// ----------------------------------------------------------------------------------------------------------

        [HttpGet]
        public IEnumerable<Course> AllCourses()
        {
            return courses;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public Course Get(int id)
        {
            var course = courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return course;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public HttpResponseMessage Post([FromBody]Course course)
        {
            course.Id = courses.Count;

            courses.Add(course);

            var response = Request.CreateResponse(HttpStatusCode.Created, course);

            var uri = Url.Link("DefaultApi", new { id = course.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void Put([FromBody]Course course)
        {
            var actualCourse = Get(course.Id);
            actualCourse.Title = course.Title;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public void Delete(int id)
        {
            var course = Get(id);

            if (course != null)
                courses.Remove(course);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }

    //// ----------------------------------------------------------------------------------------------------------
		 
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
