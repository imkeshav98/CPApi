using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<SubjectResponseDto>>> GetAllSubjects()
        {
            var serviceResponse = new ServiceResponse<List<SubjectResponseDto>>();
            try
            {
                var dbSubjects = await _subjectRepository.GetAllSubjects();
                serviceResponse.Data = _mapper.Map<List<SubjectResponseDto>>(dbSubjects);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async  Task<ServiceResponse<SubjectResponseDto>> GetSubjectById(int id)
        {
            var serviceResponse = new ServiceResponse<SubjectResponseDto>();
            try
            {
                var dbSubject = await _subjectRepository.GetSubjectById(id);
                serviceResponse.Data = _mapper.Map<SubjectResponseDto>(dbSubject);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<SubjectResponseDto>> AddSubject(SubjectRequestDto subject)
        {
            var serviceResponse = new ServiceResponse<SubjectResponseDto>();
            try
            {
                var dbSubject = await _subjectRepository.AddSubject(_mapper.Map<Subject>(subject));
                serviceResponse.Data = _mapper.Map<SubjectResponseDto>(dbSubject);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<SubjectResponseDto>> UpdateSubject(SubjectRequestDto subject, int id)
        {
            var serviceResponse = new ServiceResponse<SubjectResponseDto>();
            try
            {
                var subjectToUpdate = _mapper.Map<Subject>(subject);
                subjectToUpdate.Id = id;
                var dbSubject = await _subjectRepository.UpdateSubject(subjectToUpdate);
                serviceResponse.Data = _mapper.Map<SubjectResponseDto>(dbSubject);
            }
            catch (NotFoundException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> CloseSubject(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                serviceResponse.Data = await _subjectRepository.CloseSubject(id);
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