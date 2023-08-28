using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories.Interface
{
    public interface IMarkRepository
    {
        Task<List<Mark>> GetAllMarks();
        Task<Mark> GetMarkById(int id);
        Task<Mark> AddMark(Mark mark);
        Task<Mark> UpdateMark(Mark mark);
        Task<bool> DeleteMark(int id);
        Task<List<Mark>> GetMarksByEnrollmentId(int id);
        Task<List<Mark>> GetMarksBySubjectId(int id);
    }
}