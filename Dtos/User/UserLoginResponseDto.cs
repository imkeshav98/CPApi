using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.User
{
    public class UserLoginResponseDto
    {
       public UserResponseDto? User { get; set; }
        public string? Token { get; set; }
    }
}