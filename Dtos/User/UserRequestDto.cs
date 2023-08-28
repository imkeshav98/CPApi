using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.User
{
    public class UserRequestDto
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}