using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubjectId {get; set;}
        public bool Status {get; set;}
        public User? User { get; set; }
        public Subject? Subject { get; set; }
        public Mark? Mark { get; set; }
    }
}