using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.Enrollment
{
    public class EnrollmentResponseDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public UserResponseDto? User { get; set; }
        public SubjectResponseDto? Subject {get; set;}
    }
}