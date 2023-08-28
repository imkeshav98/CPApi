using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<UserResponseDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<UserResponseDto>>();
            try
            {
                var users = await _userRepository.GetAllUsers();
                serviceResponse.Data = _mapper.Map<List<UserResponseDto>>(users);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserResponseDto>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<UserResponseDto>();
            try
            {
                var user = await _userRepository.GetUserById(id);
                serviceResponse.Data = _mapper.Map<UserResponseDto>(user);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserResponseDto>> UpdateUser(UserRequestDto user, int id)
        {
            var serviceResponse = new ServiceResponse<UserResponseDto>();
            try
            {
                var userToUpdate = _mapper.Map<User>(user);
                userToUpdate.Id = id;
                var updatedUser = await _userRepository.UpdateUser(userToUpdate);
                serviceResponse.Data = _mapper.Map<UserResponseDto>(updatedUser);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                await _userRepository.DeleteUser(id);
                serviceResponse.Data = true;
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}