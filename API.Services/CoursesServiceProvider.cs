using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace API.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CoursesServiceProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public List<CourseDTO> GetCoursesBySemester(string semester = null) {
            return new List<CourseDTO>();
        }
    }
}
