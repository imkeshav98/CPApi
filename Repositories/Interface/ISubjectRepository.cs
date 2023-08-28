using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories.Interface
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllSubjects();
        Task<Subject> GetSubjectById(int id);
        Task<Subject> AddSubject(Subject subject);
        Task<Subject> UpdateSubject(Subject subject);
        Task<bool> CloseSubject(int id);
    }
}