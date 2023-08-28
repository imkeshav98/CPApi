using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services.Interface
{
    public interface IUserService
    {
        Task<ServiceResponse<List<UserResponseDto>>> GetAllUsers();
        Task<ServiceResponse<UserResponseDto>> GetUserById(int id);
        Task<ServiceResponse<UserResponseDto>> UpdateUser(UserRequestDto user, int id);
        Task<ServiceResponse<bool>> DeleteUser(int id);
    }
}