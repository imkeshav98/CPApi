using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories.Interface
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllEnrollments();
        Task<Enrollment> GetEnrollmentById(int id);
        Task<Enrollment> AddEnrollment(Enrollment enrollment);
        Task<bool> CloseEnrollment(int id);
        Task<List<Enrollment>> GetEnrollmentsBySubjectId(int id);
    }
}