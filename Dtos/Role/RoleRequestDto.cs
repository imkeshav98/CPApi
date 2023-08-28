using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Dtos.Role
{
    public class RoleRequestDto
    {
        public required string Name { get; set; }
    }
}