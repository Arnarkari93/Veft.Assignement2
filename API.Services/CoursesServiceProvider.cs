using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;
using API.Services.Repositories;

namespace API.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CoursesServiceProvider
    {

        private readonly AppDataContext _db = new AppDataContext();

        #region Course only related methods
        public List<CourseDTO> GetCourses()
        {
            return null;
        }

        public CourseDTO GetCourseByID(int id)
        {
            return null;
        }

        public CourseDTO AddCourse(CourseViewModel course)
        {
            return null;
        }

        public CourseDTO UpdateCourse(CourseViewModel course)
        {
            return null;
        }

        public void DeleteCourse(CourseViewModel course)
        {

        }

        /// <summary>
        /// This method gets all the courses that are thought on the semester given.
        /// If no semester is given, then the default is the current semester.
        /// </summary>
        /// <param name="semester">The semester for the filter</param>
        /// <returns>A list of courses</returns>
        public List<CourseDTO> GetCoursesBySemester(string semester = null) {
            if (String.IsNullOrWhiteSpace(semester))
            {
                semester = "20153";
            }

            var courses = (from c in _db.Courses
                           join ct in _db.CourseTemplates on c.TemplateID equals ct.ID
                           where c.Semester == semester
                           select new CourseDTO
                           {
                               ID = c.ID,
                               TemplateID = ct.TemplateID,
                               Name = ct.Name,
                               StartDate = c.StartDate,
                               EndDate = c.EndDate
                           }).ToList();

            return courses;
        }
        #endregion
        #region Course and Student related functions
        public List<StudentDTO> GetStudentInCourse(int courseID)
        {
            return null;
        }

        public void AddStudentToCourse(int courseID, StudentViewModel student)
        {

        }
        #endregion

    }
}
