using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.Mark
{
    public class MarkRequestDto
    {
        public required int Marks { get; set; }
        public int TotalMarks { get; set; }
        public required int EnrollmentId { get; set; }
    }
}