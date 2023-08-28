using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.Mark
{
    public class MarkResponseDto
    {
        public int Id { get; set; }
        public int? Marks { get; set; }
        public int? TotalMarks { get; set; }
        public EnrollmentResponseDto? Enrollment { get; set; }
    }
}