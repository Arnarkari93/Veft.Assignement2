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
            return (from course in _db.Courses
                    join courseTemplate in _db.CourseTemplates on course.TemplateID equals courseTemplate.ID
                    select new CourseDTO
                    {
                        ID = course.ID,
                        TemplateID = courseTemplate.TemplateID,
                        Name = courseTemplate.Name,
                        StartDate = course.StartDate,
                        EndDate = course.EndDate
                    }).ToList();
        }


        /// <summary>
        /// This method gets a single student with the provided id
        /// </summary>
        /// <param name="id">The id of the student</param>
        /// <returns>A course with the provided id</returns>
        public CourseDTO GetCourseByID(int id)
        {
            return (from course in _db.Courses
                    join courseTemplate in _db.CourseTemplates on course.TemplateID equals courseTemplate.ID
                    where course.ID == id
                    select new CourseDTO
                    {
                        ID = course.ID,
                        TemplateID = courseTemplate.TemplateID,
                        Name = courseTemplate.Name,
                        StartDate = course.StartDate,
                        EndDate = course.EndDate,
                    }).SingleOrDefault();
        }

        public CourseDTO AddCourse(CourseViewModel course)
        {
            // Check if the course exsists
            if (!_db.CourseTemplates.Any(x => x.TemplateID == course.CouresID))
            {
                // TODO: throw some error.
            }

            _db.Courses.Add(new Entities.Course
            {
                ID = _db.Courses.Max(x => x.ID) + 1,
                TemplateID = _db.CourseTemplates.Where(x => x.TemplateID == course.CouresID).Select(x => x.ID).Single(),
                Semester = course.Semseter,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            });
            
            _db.SaveChanges();
            return null;
        }

        public CourseDTO UpdateCourse(CourseViewModel course)
        {
            return null;
        }

        public void DeleteCourse(CourseViewModel course)
        {
            _db.Courses.Remove((from c in _db.Courses
                                join ct in _db.CourseTemplates on c.TemplateID equals ct.ID
                                where ct.TemplateID == course.CouresID
                                select c).Single());
            _db.SaveChanges();
        }

        /// <summary>
        /// This method gets all the courses that are taught on the given semster.
        /// If no semester is given, then the default is the current semester.
        /// </summary>
        /// <param name="semester">The semester for the filter</param>
        /// <returns>A list of courses</returns>
        public List<CourseDTO> GetCoursesBySemester(string semester = null)
        {
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
