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
        public CourseDetailsDTO GetCourseByID(int id)
        {
            return (from course in _db.Courses
                    join courseTemplate in _db.CourseTemplates on course.TemplateID equals courseTemplate.ID
                    where course.ID == id
                    select new CourseDetailsDTO
                    {
                        ID = course.ID,
                        TemplateID = courseTemplate.TemplateID,
                        Name = courseTemplate.Name,
                        Description = courseTemplate.Description,
                        StartDate = course.StartDate,
                        EndDate = course.EndDate
                    }).SingleOrDefault();
        }

        public CourseDetailsDTO AddCourse(CourseViewModel newCourse)
        {
            // Check if the course exists
            var courseTemplate = _db.CourseTemplates.SingleOrDefault(x => x.TemplateID == newCourse.CourseID);
            if (courseTemplate == null )
            {
                // TODO: throw some error.
            }
            Entities.Course course = new Entities.Course
            {
                ID = _db.Courses.Any() ? _db.Courses.Max(x => x.ID) + 1 : 1,
                TemplateID = courseTemplate.ID,
                Semester = newCourse.Semseter,
                StartDate = newCourse.StartDate,
                EndDate = newCourse.EndDate
            };
            _db.Courses.Add(course); 

            _db.SaveChanges();

            return new CourseDetailsDTO
            {
                ID = course.ID,
                TemplateID = courseTemplate.TemplateID,
                Name = courseTemplate.Name,
                Description = courseTemplate.Description,
                StartDate = newCourse.StartDate,
                EndDate = newCourse.EndDate
            };
        }

        public CourseDetailsDTO UpdateCourse(int courseID, UpdateCourseViewModel updatedCourse)
        {
            Entities.Course course = _db.Courses.SingleOrDefault(x => x.ID == courseID);
            if (course == null)
            {
                // todo: throw error
            }
            course.StartDate = updatedCourse.StartDate;
            course.EndDate = updatedCourse.EndDate;

            // Check if the course tamplate exists
            var courseTemplate = _db.CourseTemplates.SingleOrDefault(x => x.ID == course.TemplateID);
            if (courseTemplate == null)
            {
                // todo: throw some error
            }

            // If all is successfull, we save our changes
            _db.SaveChanges();

            return new CourseDetailsDTO
            {   
                ID = courseID, 
                TemplateID = courseTemplate.TemplateID,
                Name = courseTemplate.Name,
                Description = courseTemplate.Description,
                StartDate = updatedCourse.StartDate,
                EndDate = updatedCourse.EndDate,
                StudentCount = _db.StudentEnrollment.Count(x => x.CourseID == courseID)
            };
        }

        public void DeleteCourse(CourseViewModel course)
        {
            _db.Courses.Remove((from c in _db.Courses
                                join ct in _db.CourseTemplates on c.TemplateID equals ct.ID
                                where ct.TemplateID == course.CourseID
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
            return (from se in _db.StudentEnrollment
                                   join s in _db.Students on se.StudentID equals s.ID
                                   where se.CourseID == courseID
                                   select new StudentDTO
                                   {
                                        SSN = s.SSN,
                                        Name = s.Name    
                                   }).ToList();
        }

        public StudentDTO AddStudentToCourse(int courseID, StudentViewModel newStudent)
        {
            // Check if the course exists
            var course = _db.Courses.SingleOrDefault(x => x.ID == courseID);
            if (course == null)
            {
                // Todo: throw error
            }

            // Check if the student exists
            var student = _db.Students.SingleOrDefault(x => x.SSN == newStudent.SSN);
            if (student == null)
            {
                // todo : throw error
            }

            _db.StudentEnrollment.Add(new Entities.StudentEnrollment
            {
                StudentID = student.ID,
                CourseID = course.ID
            });

            _db.SaveChanges();

            return new StudentDTO
            {
                Name = student.Name,
                SSN = student.SSN
            };
        }
        #endregion

    }
}
