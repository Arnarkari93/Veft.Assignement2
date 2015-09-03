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
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
