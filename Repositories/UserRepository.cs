using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            // get user from db and also include role
            var dbUsers = await _context.Users.Include(u => u.Role).ToListAsync();
            return dbUsers;
        }

        public async Task<User> GetUserById(int id)
        {
            var dbUser = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
            if(dbUser is null)
            {
                throw new NotFoundException("User not found");
            }
            return dbUser;
        }

        public async Task<User> UpdateUser(User user)
        {
            var dbUser = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == user.Id);
            if(dbUser is null)
            {
                throw new NotFoundException("User not found");
            }
            dbUser.Name = string.IsNullOrEmpty(user.Name) ? dbUser.Name : user.Name;
            dbUser.Username = string.IsNullOrEmpty(user.Username) ? dbUser.Username : user.Username;
            dbUser.BirthDate = user.BirthDate ?? dbUser.BirthDate;
            await _context.SaveChangesAsync();
            return dbUser;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if(dbUser is null)
            {
                throw new NotFoundException("User not found");
            }
            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}