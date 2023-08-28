using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services.Interface
{
    public interface IMarkService
    {
        Task<ServiceResponse<List<MarkResponseDto>>> GetAllMarks();
        Task<ServiceResponse<MarkResponseDto>> GetMarkById(int id);
        Task<ServiceResponse<MarkResponseDto>> AddMark(MarkRequestDto mark);
        Task<ServiceResponse<MarkResponseDto>> UpdateMark(MarkRequestDto mark, int id);
        Task<ServiceResponse<bool>> DeleteMark(int id);
        Task<ServiceResponse<List<MarkResponseDto>>> GetMarksByEnrollmentId(int id);
        Task<ServiceResponse<List<MarkResponseDto>>> GetMarksBySubjectId(int id);
    }
}