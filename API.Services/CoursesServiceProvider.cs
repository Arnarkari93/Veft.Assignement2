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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
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
    }
}
