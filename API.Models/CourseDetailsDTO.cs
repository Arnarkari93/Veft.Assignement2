using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    /// <summary>
    /// This class represnets a single course, and contains various details about the course
    /// </summary>
    class CourseDetailsDTO
    {
        /// <summary>
        /// Id of the course
        /// Example: T-514-VEFT
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the course
        /// Example: Web services
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description for a single course
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The count of Students in the course
        /// </summary>
        public int StudentCount { get; set; }

    }
}
