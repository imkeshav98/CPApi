using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public int RoleId { get; set; }

        public Role? Role { get; set; }
        public List<Subject>? Subjects { get; set; }
        public List<Enrollment>? Enrollments { get; set; }
    }
}