using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories.Interface
{
    public interface IAuthRepository
    {
        Task<int> Register(User user);
        Task<User> Login(User user);
        Task<bool> UserExists(string username);
    }
}