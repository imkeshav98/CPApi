namespace CPApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<RoleResponseDto>>> GetAllRoles()
        {
            var serviceResponse = new ServiceResponse<List<RoleResponseDto>>();
            try
            {
                var roles = await _roleRepository.GetAllRoles();
                serviceResponse.Data = _mapper.Map<List<RoleResponseDto>>(roles);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<RoleResponseDto>> GetRoleById(int id)
        {
            var serviceResponse = new ServiceResponse<RoleResponseDto>();
            try
            {
                var role = await _roleRepository.GetRoleById(id);
                serviceResponse.Data = _mapper.Map<RoleResponseDto>(role);
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