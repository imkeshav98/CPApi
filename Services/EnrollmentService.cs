using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMarkRepository _markRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IMarkRepository markRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _enrollmentRepository = enrollmentRepository;
            _markRepository = markRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<EnrollmentResponseDto>>> GetAllEnrollments()
        {
            var serviceResponse = new ServiceResponse<List<EnrollmentResponseDto>>();
            try 
            {
                var dbEnrollments = await _enrollmentRepository.GetAllEnrollments();
                serviceResponse.Data = _mapper.Map<List<EnrollmentResponseDto>>(dbEnrollments);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<EnrollmentResponseDto>> GetEnrollmentById(int id)
        {
            var serviceResponse = new ServiceResponse<EnrollmentResponseDto>();
            try 
            {
                var dbEnrollment = await _enrollmentRepository.GetEnrollmentById(id);
                serviceResponse.Data = _mapper.Map<EnrollmentResponseDto>(dbEnrollment);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }


        public async Task<ServiceResponse<EnrollmentResponseDto>> AddEnrollment(EnrollmentRequestDto enrollment)
        {
            var serviceResponse = new ServiceResponse<EnrollmentResponseDto>();
            try 
            {
                var dbEnrollment = await _enrollmentRepository.AddEnrollment(_mapper.Map<Enrollment>(enrollment));
                serviceResponse.Data = _mapper.Map<EnrollmentResponseDto>(dbEnrollment);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> CloseEnrollment(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            var enrollment = await _enrollmentRepository.GetEnrollmentsBySubjectId(id);
            var marks = await _markRepository.GetMarksByEnrollmentId(id);

            if (enrollment.Status == false)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Enrollment is already closed";
                return serviceResponse;
            }

            if (marks.Count == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Marks are not added yet for this enrollment";
                return serviceResponse;
            }
            try 
            {
                serviceResponse.Data = await _enrollmentRepository.CloseEnrollment(id);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        // method for GetEnrollmentsBySubjectId
        public async Task<ServiceResponse<List<EnrollmentResponseDto>>> GetEnrollmentsBySubjectId(int subjectId)
        {
            var serviceResponse = new ServiceResponse<List<EnrollmentResponseDto>>();
            try 
            {
                var dbEnrollments = await _enrollmentRepository.GetEnrollmentsBySubjectId(subjectId);
                serviceResponse.Data = _mapper.Map<List<EnrollmentResponseDto>>(dbEnrollments);
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