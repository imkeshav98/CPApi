using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.Enrollment
{
    public class EnrollmentRequestDto
    {
        public int SubjectId { get; set; }
        public bool Status { get; set; } = true;
    }
}