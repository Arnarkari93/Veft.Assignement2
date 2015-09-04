using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    public class CourseViewModel
    {
        /// <summary>
        /// Example: T-514-VEFT
        /// </summary>
        [Required]
        public string CourseID { get; set; }

        /// <summary>
        /// Example: 20151
        /// </summary>
        [Required]
        public string Semseter { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
