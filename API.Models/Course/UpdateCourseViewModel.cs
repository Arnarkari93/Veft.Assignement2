using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    /// <summary>
    /// This class represents what is allow
    /// </summary>
    public class UpdateCourseViewModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
