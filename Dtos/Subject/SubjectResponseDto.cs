using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.Subject
{
    public class SubjectResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool Status { get; set; }
        public UserResponseDto? User { get; set; }
        public SemesterResponseDto? Semester { get; set; }
    }
}