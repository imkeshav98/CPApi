using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.Subject
{
    public class SubjectRequestDto
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required int SemesterId { get; set; }
        public bool Status { get; set; } = true;
    }
}