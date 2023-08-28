using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services.Interface
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegisterDto user);
        Task<ServiceResponse<UserLoginResponseDto>> Login(UserLoginDto user);
        Task<bool> UserExists(string username);
        
    }
}