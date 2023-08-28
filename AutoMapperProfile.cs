using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Role Mapping
            CreateMap<Role, RoleResponseDto>();
            CreateMap<RoleRequestDto, Role>();

            // User mapping
            CreateMap<User, UserResponseDto>();
            CreateMap<UserRequestDto, User>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserLoginDto, User>();
            CreateMap<User, UserLoginResponseDto>();

            // Subject mapping
            CreateMap<Subject, SubjectResponseDto>();
            CreateMap<SubjectRequestDto, Subject>();

            //Semester mapping
            CreateMap<Semester, SemesterResponseDto>();
            CreateMap<SemesterRequestDto, Semester>();

            //Mark mapping
            CreateMap<Mark, MarkResponseDto>();
            CreateMap<MarkRequestDto, Mark>();

            //Enrollment mapping
            CreateMap<Enrollment, EnrollmentResponseDto>();
            CreateMap<EnrollmentRequestDto, Enrollment>();
            
        }
     
    }
}