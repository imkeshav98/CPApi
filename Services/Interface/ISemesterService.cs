using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services.Interface
{
    public interface ISemesterService
    {
        Task<ServiceResponse<List<SemesterResponseDto>>> GetAllSemesters();
        Task<ServiceResponse<SemesterResponseDto>> GetSemesterById(int id);
        Task<ServiceResponse<SemesterResponseDto>> UpdateSemester(SemesterRequestDto semester, int id);
        Task<ServiceResponse<SemesterResponseDto>> AddSemester(SemesterRequestDto semester);
        Task<ServiceResponse<bool>> CloseSemester(int id);
    }
}