using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int Marks { get; set; }
        public int TotalMarks { get; set; }
        public int EnrollmentId { get; set; }

        public Enrollment? Enrollment { get; set; }
    }
}