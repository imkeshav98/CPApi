using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code {get; set;}
        public int UserId { get; set; }
        public int SemesterId { get; set; }
        public bool Status { get; set; } 

        public User? User { get; set; }
        public Semester? Semester { get; set; }
        public List<Enrollment>? Enrollments { get; set; }
    }
}