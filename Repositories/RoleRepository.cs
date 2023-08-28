namespace CPApi.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var dbRoles = await _context.Roles.ToListAsync();
            return dbRoles;
        }

        public async Task<Role> GetRoleById(int id)
        {
            var dbRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (dbRole == null)
            {
                throw new NotFoundException("Role not found");
            }
            return dbRole;
        }

        public async Task<int> GetRoleByName(string name)
        {
            var dbRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
            if (dbRole == null)
            {
                throw new NotFoundException("Role not found");
            }
            return dbRole.Id;
        }
    }
}