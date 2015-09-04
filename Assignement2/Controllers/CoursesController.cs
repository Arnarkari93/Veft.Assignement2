using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Assignment2.Controllers
{
    [RoutePrefix("api/v1/courses")]
    public class CoursesController : ApiController
    {
        #region Private methods and properties

        private static List<CourseDTO>_courses;

        /// <summary>
        /// This method gets the course with the given id.
        /// It whould be better to use filter to handle these exceptions but it is
        /// required in this assigment.
        /// </summary>
        /// <param name="id">id of the course</param>
        /// <returns>a single course</returns>
        private CourseDTO _GetCourseById(int id)
        {
            // Throw error if there are no courses
            if (_courses == null) { throw new HttpResponseException(HttpStatusCode.NotFound); }

            // Get the course
            var course = _courses.Where(x => x.ID == id).SingleOrDefault();
            // Throw error if the course dose not exsits
            if (course == null) { throw new HttpResponseException(HttpStatusCode.NotFound); }

            return course;
        }
        #endregion

        #region public methods
        //public CoursesController()
        //{
        //    if (_courses == null)
        //    {
        //        _courses = new List<Course>
        //        {
        //            new Course
        //            {
        //                ID = 1,
        //                Name =  "Web services",
        //                TemplateID = "T-514-VEFT",
        //                StartDate = DateTime.Now,
        //                EndDate = DateTime.Now.AddMonths(3),
        //                Students = new List<Student>
        //                {
        //                    new Student
        //                    {
        //                        Name = "Mike Cohn",
        //                        SSN = "1209626666"
        //                    },
        //                    new Student
        //                    {
        //                        Name = "Renee Cohn",
        //                        SSN = "0606726666"
        //                    }
        //                }
        //            },
        //            new Course
        //            {
        //                ID = 2,
        //                Name =  "Computer Networks",
        //                TemplateID = "T-409-TSAM",
        //                StartDate = DateTime.Now,
        //                EndDate = DateTime.Now.AddMonths(3),
        //                Students = new List<Student>
        //                {
        //                    new Student
        //                    {
        //                        Name = "Mike Cohn",
        //                        SSN = "1209626666"
        //                    },
        //                    new Student
        //                    {
        //                        Name = "Renee Cohn",
        //                        SSN = "0606726666"
        //                    }
        //                }
        //            },
        //            new Course
        //            {
        //                ID = 3,
        //                Name =  "Software Design and Implementation",
        //                TemplateID = "T-302-HONN",
        //                StartDate = DateTime.Now,
        //                EndDate = DateTime.Now.AddMonths(3)
        //            }
        //        };
        //    }
        //}

        ///// <summary>
        ///// This method gets a list of avalible courses
        ///// </summary>
        ///// <returns>A list of all courses</returns>
        //[HttpGet]
        //[Route("")]
        //public IHttpActionResult GetCourses()
        //{
        //    return Ok(_courses);
        //}

        ///// <summary>
        ///// This method addes a new course the list of courses
        ///// </summary>
        ///// <param name="newCourse">The new course</param>
        ///// <returns>The location of the new course</returns>
        //[HttpPost]
        //[Route("")]
        //public IHttpActionResult AddCourse(Course newCourse)
        //{
        //    if (!ModelState.IsValid) { throw new HttpResponseException(HttpStatusCode.PreconditionFailed); }

        //    newCourse.ID = _courses.Any() ? _courses.Max(x => x.ID) + 1 : 0; // get a new id
            
        //    if (newCourse == null) { throw new HttpResponseException(HttpStatusCode.PreconditionFailed); }
        //    // adding course to the list
        //    _courses.Add(newCourse);
        //    // get the location 
        //    var location = Url.Link("GetCourse", new { id = newCourse.ID });

        //    return Created(location, newCourse);
        //}

        ///// <summary>
        ///// This method updates the given course properties for a single given course
        ///// </summary>
        ///// <param name="editedCourse">The edited course</param>
        ///// <returns>200 if successful</returns>
        //[HttpPut]
        //[Route("{id:int}")]
        //[ResponseType(typeof(Course))]
        //public IHttpActionResult UpdateCourse(int id, Course editedCourse)
        //{
        //    if (!ModelState.IsValid) { throw new HttpResponseException(HttpStatusCode.PreconditionFailed); }

        //    var index = _courses.FindIndex(x => x.ID == id);
        //    if (index == -1) { throw new HttpResponseException(HttpStatusCode.NotFound); }
        //    editedCourse.ID = id;
        //    _courses[index] = editedCourse;

        //    return Ok();
        //}

        ///// <summary>
        ///// This method deletes the course with the given id   
        ///// </summary>
        ///// <param name="id">The id of the course</param>
        //[HttpDelete]
        //[Route("{id:int}", Name="DeleteCourse" )]
        //public void DeleteCourse(int id)
        //{
        //    var course = _GetCourseById(id); // this may throw a not found exception
        //    _courses.Remove(course);
        //}

        ///// <summary>
        ///// This method gets the course with the given id
        ///// </summary>
        ///// <param name="id">id of the course</param>
        ///// <returns>A single course</returns>
        //[HttpGet]
        //[Route("{id:int}", Name="GetCourse")]
        //public IHttpActionResult GetCouse(int id)
        //{
        //    return Ok(_GetCourseById(id));
        //}

        ///// <summary>
        ///// Get a list of all the students in a given course
        ///// </summary>
        ///// <param name="id">id of the course</param>
        ///// <returns>List of students in the course</returns>
        //[HttpGet]
        //[Route("{id:int}/students", Name="GetStudentsInCourse")]
        //public IHttpActionResult GetStudentsInCourse(int id)
        //{
        //    var course = _GetCourseById(id); // this may throw a not found exception
        //    if (course.Students == null) { throw new HttpResponseException(HttpStatusCode.NotFound); }
        //    return Ok(course.Students);
        //}

        ///// <summary>
        ///// Adds a student to a given course
        ///// </summary>
        ///// <param name="id">id of the course</param>
        ///// <param name="newStudent">The student to be added</param>
        ///// <returns>List of the students in the course with the added student</returns>
        //[HttpPost]
        //[Route("{id:int}/students", Name="AddStudentToCourse")]
        //public IHttpActionResult AddStudentToCourse(int id, Student newStudent)
        //{
        //    if (!ModelState.IsValid) { throw new HttpResponseException(HttpStatusCode.PreconditionFailed); }

        //    var course = _GetCourseById(id);
        //    if (course.Students == null) { course.Students = new List<Student>(); }  
        //    course.Students.Add(newStudent); // adding student to the list of students for the course
        //    var location = Url.Link("GetStudentsInCourse", new { id = course.ID });

        //    return Created(location, course.Students);
        //}
        #endregion
    }
}
