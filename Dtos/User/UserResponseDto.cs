using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public RoleResponseDto? Role { get; set; }
    }
}