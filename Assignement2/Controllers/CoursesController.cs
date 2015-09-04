using API.Models;
using API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Assignment2.Controllers
{
    [RoutePrefix("api/v1/courses")]
    public class CoursesController : ApiController
    {
        #region Private methods and properties

        private static CoursesServiceProvider _service;
        private static List<CourseDTO>_courses;

        /// <summary>
        /// This method gets the course with the given id.
        /// It whould be better to use filter to handle these exceptions but it is
        /// required in this assigment.
        /// </summary>
        /// <param name="id">id of the course</param>
        /// <returns>a single course</returns>
        private IHttpActionResult _GetCourseById(int id)
        {
            CourseDetailsDTO course = _service.GetCourseByID(id);
            if (course == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok(course);
        }
        #endregion

        #region public methods

        /// <summary>
        /// This method gets a list of avalible courses
        /// </summary>
        /// <returns>A list of all courses</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCourses()
        {
            return Ok(_service.GetCourses());
        }

        /// <summary>
        /// This method addes a new course the list of courses
        /// </summary>
        /// <param name="newCourse">The new course</param>
        /// <returns>The location of the new course</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddCourse(CourseViewModel newCourse)
        {
            if (!ModelState.IsValid) { throw new HttpResponseException(HttpStatusCode.PreconditionFailed); }

            CourseDetailsDTO course = _service.AddCourse(newCourse); // todo catch error

            var location = Url.Link("GetCourse", new { id = course.ID });

            return Created(location, course);
        }

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
