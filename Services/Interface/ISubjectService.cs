using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services.Interface
{
    public interface ISubjectService
    {
        Task<ServiceResponse<List<SubjectResponseDto>>> GetAllSubjects();
        Task<ServiceResponse<SubjectResponseDto>> GetSubjectById(int id);
        Task<ServiceResponse<SubjectResponseDto>> UpdateSubject(SubjectRequestDto subject, int id);
        Task<ServiceResponse<SubjectResponseDto>> AddSubject(SubjectRequestDto subject);
        Task<ServiceResponse<bool>> CloseSubject(int id);
    }
}