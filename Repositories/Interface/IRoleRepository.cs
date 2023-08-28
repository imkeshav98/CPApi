namespace CPApi.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);   
        Task<int> GetRoleByName(string name);
    }
}