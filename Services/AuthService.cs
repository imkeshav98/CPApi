using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CPApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IAuthRepository authRepository, IMapper mapper, IConfiguration configuration, IRoleRepository roleRepository)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _configuration = configuration;
            _roleRepository = roleRepository;
        }

        public async Task<ServiceResponse<UserLoginResponseDto>> Login(UserLoginDto user)
        {
            var serviceResponse = new ServiceResponse<UserLoginResponseDto>();
            try{
                var loginUser = _mapper.Map<User>(user);
                var userFromRepo = await _authRepository.Login(loginUser);

                if(!VerifyPasswordHash(user.Password, userFromRepo.PasswordHash, userFromRepo.PasswordSalt))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Invalid username or password.";
                }
                else
                {
                    serviceResponse.Data = _mapper.Map<UserLoginResponseDto>(userFromRepo);
                    serviceResponse.Data.Token = CreateToken(userFromRepo);
                    serviceResponse.Data.User = _mapper.Map<UserResponseDto>(userFromRepo);
                }
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> Register(UserRegisterDto user)
        {
            var serviceResponse = new ServiceResponse<int>();
            try
            {
                // check if user exists
                if (await UserExists(user.Username))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User already exists.";
                    return serviceResponse;
                }
                // get role by email
                var roleName = GetRoleByEmail(user.Email);
                var roleId = await _roleRepository.GetRoleByName(roleName);

                // create password hash
                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                
                var userToCreate = _mapper.Map<User>(user);
                userToCreate.PasswordHash = passwordHash;
                userToCreate.PasswordSalt = passwordSalt;
                userToCreate.RoleId = roleId;

                // register user
                var createdUserId = await _authRepository.Register(userToCreate);
                serviceResponse.Data = createdUserId;
            }
            catch (InvalidEmailException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _authRepository.UserExists(username);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string GetRoleByEmail(string email)
        {
            var roleMappings = _configuration.GetSection("RoleConfiguration:Mappings")
                                            .Get<Dictionary<string, string>>() ?? new Dictionary<string, string>();

            foreach (var mapping in roleMappings)
            {
                if (email.EndsWith(mapping.Key))
                {
                    return mapping.Value;
                }
            }
            throw new InvalidEmailException("Invalid email. Use email provided by the organization.");
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role!.Name!)
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
            {
                throw new Exception("Token is null!");
            }
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(appSettingsToken));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}