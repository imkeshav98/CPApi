using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly DataContext _context;
        private readonly IUserContext _userContext;

        public EnrollmentRepository(DataContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<List<Enrollment>> GetAllEnrollments()
        {
            var userId = _userContext.GetUserId();
            var userRole = _userContext.GetUserRole();
            var dbEnrollments = new List<Enrollment>();
            if(userRole == AvailableRoles.Student)
            {
                dbEnrollments = await _context.Enrollments
                    .Include(e => e.User)
                    .Include(s => s.User!.Role)
                    .Include(e => e.Subject)
                    .Include(e => e.Subject!.User)
                    .Include(e => e.Subject!.User!.Role)
                    .Include(e => e.Subject!.Semester)
                    .Include(e => e.Mark)
                    .Where(e => e.UserId == userId)
                    .ToListAsync();
            }
            else if(userRole == AvailableRoles.Teacher)
            {
                dbEnrollments = await _context.Enrollments
                    .Include(e => e.User)
                    .Include(s => s.User!.Role)
                    .Include(e => e.Subject)
                    .Include(e => e.Subject!.User)
                    .Include(e => e.Subject!.User!.Role)
                    .Include(e => e.Subject!.Semester)
                    .Include(e => e.Mark)
                    .Where(e => e.Subject!.UserId == userId)
                    .ToListAsync();
            }
            else
            {
                dbEnrollments = await _context.Enrollments
                    .Include(e => e.User)
                    .Include(s => s.User!.Role)
                    .Include(e => e.Subject)
                    .Include(e => e.Subject!.User)
                    .Include(e => e.Subject!.User!.Role)
                    .Include(e => e.Subject!.Semester)
                    .Include(e => e.Mark)
                    .ToListAsync();
            }
            return dbEnrollments;
        }

        public async Task<Enrollment> GetEnrollmentById(int id)
        {
            var userId = _userContext.GetUserId();
            var userRole = _userContext.GetUserRole();
            var dbEnrollment = new Enrollment();
            if(userRole == AvailableRoles.Student)
            {
                dbEnrollment = await _context.Enrollments
                    .Include(e => e.User)
                    .Include(s => s.User!.Role)
                    .Include(e => e.Subject)
                    .Include(e => e.Subject!.User)
                    .Include(e => e.Subject!.User!.Role)
                    .Include(e => e.Subject!.Semester)
                    .Include(e => e.Mark)
                    .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            }
            else if(userRole == AvailableRoles.Teacher)
            {
                dbEnrollment = await _context.Enrollments
                    .Include(e => e.User)
                    .Include(s => s.User!.Role)
                    .Include(e => e.Subject)
                    .Include(e => e.Subject!.User)
                    .Include(e => e.Subject!.User!.Role)
                    .Include(e => e.Subject!.Semester)
                    .Include(e => e.Mark)
                    .FirstOrDefaultAsync(e => e.Id == id && e.Subject!.UserId == userId);
            }
            else
            {
                dbEnrollment = await _context.Enrollments
                    .Include(e => e.User)
                    .Include(s => s.User!.Role)
                    .Include(e => e.Subject)
                    .Include(e => e.Subject!.User)
                    .Include(e => e.Subject!.User!.Role)
                    .Include(e => e.Subject!.Semester)
                    .Include(e => e.Mark)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            if(dbEnrollment is null)
            {
                throw new NotFoundException("Enrollment not found");
            }
            return dbEnrollment;
        }

        public async Task<Enrollment> AddEnrollment(Enrollment enrollment)
        {
            var userId = _userContext.GetUserId();
            enrollment.UserId = userId;
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            var dbEnrollment = await _context.Enrollments
                .Include(e => e.User)
                .Include(s => s.User!.Role)
                .Include(e => e.Subject)
                .Include(e => e.Subject!.User)
                .Include(e => e.Subject!.User!.Role)
                .Include(e => e.Subject!.Semester)
                .Include(e => e.Mark)
                .FirstOrDefaultAsync(e => e.Id == enrollment.Id);
            if(dbEnrollment is null)
            {
                throw new Exception("Unable to add enrollment");
            }
            return dbEnrollment;
        }

        public async Task<bool> CloseEnrollment(int id)
        {
            var dbEnrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == id);
            if(dbEnrollment is null)
            {
                throw new NotFoundException("Enrollment not found");
            }
            dbEnrollment.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Enrollment>> GetEnrollmentsBySubjectId(int id)
        {
            var userId = _userContext.GetUserId(); 
            var dbEnrollments = await _context.Enrollments
                .Include(e => e.User)
                .Include(s => s.User!.Role)
                .Include(e => e.Subject)
                .Include(e => e.Subject!.User)
                .Include(e => e.Subject!.User!.Role)
                .Include(e => e.Subject!.Semester)
                .Include(e => e.Mark)
                .Where(e => e.SubjectId == id)
                .ToListAsync();
            return dbEnrollments;
        }
    }
}