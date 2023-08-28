using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services.Interface
{
    public interface IEnrollmentService
    {
        Task<ServiceResponse<List<EnrollmentResponseDto>>> GetAllEnrollments();
        Task<ServiceResponse<EnrollmentResponseDto>> GetEnrollmentById(int id);
        Task<ServiceResponse<EnrollmentResponseDto>> AddEnrollment(EnrollmentRequestDto enrollment);
        Task<ServiceResponse<bool>> CloseEnrollment(int id);
        Task<ServiceResponse<List<EnrollmentResponseDto>>> GetEnrollmentsBySubjectId(int id);
    }
}