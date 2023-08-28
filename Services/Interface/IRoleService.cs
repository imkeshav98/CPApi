namespace CPApi.Services.Interface
{
    public interface IRoleService
    {
        Task<ServiceResponse<List<RoleResponseDto>>> GetAllRoles();
        Task<ServiceResponse<RoleResponseDto>> GetRoleById(int id);
    }
}