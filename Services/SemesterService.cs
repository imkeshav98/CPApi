using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services
{
    public class SemesterService : ISemesterService
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly IMapper _mapper;

        public SemesterService(ISemesterRepository semesterRepository, IMapper mapper)
        {
            _semesterRepository = semesterRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<SemesterResponseDto>>> GetAllSemesters()
        {
            var serviceResponse = new ServiceResponse<List<SemesterResponseDto>>();
            try
            {
                var semesters = await _semesterRepository.GetAllSemesters();
                serviceResponse.Data = _mapper.Map<List<SemesterResponseDto>>(semesters);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<SemesterResponseDto>> GetSemesterById(int id)
        {
            var serviceResponse = new ServiceResponse<SemesterResponseDto>();
            try
            {
                var semester = await _semesterRepository.GetSemesterById(id);
                serviceResponse.Data = _mapper.Map<SemesterResponseDto>(semester);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<SemesterResponseDto>> AddSemester(SemesterRequestDto semester)
        {
            var serviceResponse = new ServiceResponse<SemesterResponseDto>();
            try
            {
                var semesterToAdd = _mapper.Map<Semester>(semester);
                var addedSemester = await _semesterRepository.AddSemester(semesterToAdd);
                serviceResponse.Data = _mapper.Map<SemesterResponseDto>(addedSemester);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<SemesterResponseDto>> UpdateSemester(SemesterRequestDto semester, int id)
        {
            var serviceResponse = new ServiceResponse<SemesterResponseDto>();
            try
            {
                var semesterToUpdate = _mapper.Map<Semester>(semester);
                semesterToUpdate.Id = id;
                var updatedSemester = await _semesterRepository.UpdateSemester(semesterToUpdate);
                serviceResponse.Data = _mapper.Map<SemesterResponseDto>(updatedSemester);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> CloseSemester(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var closedSemester = await _semesterRepository.CloseSemester(id);
                serviceResponse.Data = closedSemester;
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