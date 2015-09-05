using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Entities
{
    /// <summary>
    /// This class represents a student and course relationship
    /// </summary>
    [Table("StudentEnrollment")]
    class StudentEnrollment
    {
        /// <summary>
        /// The Id of the enrollment
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The ID of a student in the course
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// The ID of the course the student is enrolled in
        /// </summary>
        public int CourseID { get; set; }
    }
}
