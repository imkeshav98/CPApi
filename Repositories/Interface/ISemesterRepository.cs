using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories.Interface
{
    public interface ISemesterRepository
    {
        Task<List<Semester>> GetAllSemesters();
        Task<Semester> GetSemesterById(int id);
        Task<Semester> AddSemester(Semester semester);
        Task<Semester> UpdateSemester(Semester semester);
        Task<bool> CloseSemester(int id);
    }
}