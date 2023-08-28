using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.User
{
    public class UserRegisterDto
    {
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string Password { get; set; }
    }
}