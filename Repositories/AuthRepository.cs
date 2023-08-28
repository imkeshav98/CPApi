using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
            
        }

        public async Task<User> Login(User user)
        {
            var dbUser = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Username.ToLower()
            .Equals(user.Username.ToLower()));
            if(dbUser == null){
                throw new NotFoundException("Invalid username or password");
            }
            return dbUser;
        }

        public async Task<int> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
                return true;
            return false;
        }
    }
}