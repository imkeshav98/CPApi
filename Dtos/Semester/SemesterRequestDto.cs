using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.Semester
{
    public class SemesterRequestDto
    {
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate{ get; set; }
        public bool Status { get; set; } = true;
    }
}