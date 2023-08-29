using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services
{
    public class MarkService : IMarkService
    {
        private readonly IMarkRepository _markRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public MarkService(IMarkRepository markRepository, IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _markRepository = markRepository;
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<MarkResponseDto>>> GetAllMarks()
        {
            var serviceResponse = new ServiceResponse<List<MarkResponseDto>>();
            try
            {
                var dbMarks = await _markRepository.GetAllMarks();
                serviceResponse.Data = _mapper.Map<List<MarkResponseDto>>(dbMarks);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<MarkResponseDto>> GetMarkById(int id)
        {
            var serviceResponse = new ServiceResponse<MarkResponseDto>();
            try
            {
                var dbMark = await _markRepository.GetMarkById(id);
                serviceResponse.Data = _mapper.Map<MarkResponseDto>(dbMark);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<MarkResponseDto>> AddMark(MarkRequestDto mark)
        {
            var serviceResponse = new ServiceResponse<MarkResponseDto>();
            try
            {
                var dbMark = await _markRepository.AddMark(_mapper.Map<Mark>(mark));
                var closeEnrollment = await _enrollmentRepository.CloseEnrollment(mark.EnrollmentId);
                serviceResponse.Data = _mapper.Map<MarkResponseDto>(dbMark);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<MarkResponseDto>> UpdateMark(MarkRequestDto mark, int id)
        {
            var serviceResponse = new ServiceResponse<MarkResponseDto>();
            try
            {
                var markToUpdate = _mapper.Map<Mark>(mark);
                markToUpdate.Id = id;
                var dbMark = await _markRepository.UpdateMark(markToUpdate);
                serviceResponse.Data = _mapper.Map<MarkResponseDto>(dbMark);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<bool>> DeleteMark(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                serviceResponse.Data = await _markRepository.DeleteMark(id);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MarkResponseDto>>> GetMarksByEnrollmentId(int id)
        {
            var serviceResponse = new ServiceResponse<List<MarkResponseDto>>();
            try
            {
                var dbMarks = await _markRepository.GetMarksByEnrollmentId(id);
                serviceResponse.Data = _mapper.Map<List<MarkResponseDto>>(dbMarks);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MarkResponseDto>>> GetMarksBySubjectId(int id)
        {
            var serviceResponse = new ServiceResponse<List<MarkResponseDto>>();
            try
            {
                var dbMarks = await _markRepository.GetMarksBySubjectId(id);
                serviceResponse.Data = _mapper.Map<List<MarkResponseDto>>(dbMarks);
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