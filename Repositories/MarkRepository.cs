using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories
{
    public class MarkRepository : IMarkRepository
    {
        private readonly DataContext _context;
        private readonly IUserContext _userContext;

        public MarkRepository(DataContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }
        public async Task<List<Mark>> GetAllMarks()
        {
            var userId = _userContext.GetUserId();
            var dbMarks = await _context.Marks
                    .Include(m => m.Enrollment)
                    .Include(m => m.Enrollment!.User)
                    .Include(m => m.Enrollment!.User!.Role)
                    .Include(m => m.Enrollment!.Subject)
                    .Include(m => m.Enrollment!.Subject!.User)
                    .Include(m => m.Enrollment!.Subject!.User!.Role)
                    .Include(m => m.Enrollment!.Subject!.Semester)
                    .Where(m => m.Enrollment!.UserId == userId)
                    .ToListAsync();
            if(dbMarks is null)
            {
                throw new NotFoundException("Marks not found");
            }
            return dbMarks;
        }

        public async Task<Mark> GetMarkById(int id)
        {
            var userId = _userContext.GetUserId();
            var dbMark = await _context.Marks
                    .Include(m => m.Enrollment)
                    .Include(m => m.Enrollment!.User)
                    .Include(m => m.Enrollment!.User!.Role)
                    .Include(m => m.Enrollment!.Subject)
                    .Include(m => m.Enrollment!.Subject!.User)
                    .Include(m => m.Enrollment!.Subject!.User!.Role)
                    .Include(m => m.Enrollment!.Subject!.Semester)
                    .Where(m => m.Enrollment!.UserId == userId)
                    .FirstOrDefaultAsync(m => m.Id == id && m.Enrollment!.UserId == userId);
            if(dbMark is null)
            {
                throw new NotFoundException("Mark not found");
            }
            return dbMark;
        }

        // user with role teacher can add marks in only his subjects
        public async Task<Mark> AddMark(Mark mark)
        {
            var userId = _userContext.GetUserId();
            var dbEnrollment = await _context.Enrollments
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(e => e.Id == mark.EnrollmentId && e.Subject!.UserId == userId);
            if(dbEnrollment is null)
            {
                throw new NotFoundException("Enrollment not found");
            }
            _context.Marks.Add(mark);
            await _context.SaveChangesAsync();
            var dbMark = await _context.Marks
                .Include(m => m.Enrollment)
                .Include(m => m.Enrollment!.User)
                .Include(m => m.Enrollment!.User!.Role)
                .Include(m => m.Enrollment!.Subject)
                .Include(m => m.Enrollment!.Subject!.User)
                .Include(m => m.Enrollment!.Subject!.User!.Role)
                .Include(m => m.Enrollment!.Subject!.Semester)
                //.Where(m => m.Enrollment!.UserId == userId)
                .FirstOrDefaultAsync(m => m.Id == mark.Id && m.Enrollment!.Subject!.UserId == userId);
            if(dbMark is null)
            {
                throw new Exception("Error while adding mark");
            }
            return dbMark;
        }

        // user with role teacher can update marks in only his subjects
        public async Task<Mark> UpdateMark(Mark mark)
        {
            var userId = _userContext.GetUserId();
            var dbMark = await _context.Marks
                .Include(m => m.Enrollment)
                .Include(m => m.Enrollment!.User)
                .Include(m => m.Enrollment!.User!.Role)
                .Include(m => m.Enrollment!.Subject)
                .Include(m => m.Enrollment!.Subject!.User)
                .Include(m => m.Enrollment!.Subject!.User!.Role)
                .Include(m => m.Enrollment!.Subject!.Semester)
                // .Where(m => m.Enrollment!.UserId == userId)
                .FirstOrDefaultAsync(m => m.Id == mark.Id && m.Enrollment!.Subject!.UserId == userId);
            if(dbMark is null)
            {
                throw new NotFoundException("Mark not found");
            }
            dbMark.Marks = mark.Marks;
            dbMark.TotalMarks = mark.TotalMarks;
            await _context.SaveChangesAsync();
            return dbMark;
        }

        // user with role teacher can delete marks in only his subjects
        public async Task<bool> DeleteMark(int id)
        {
            var userId = _userContext.GetUserId();
            var dbMark = await _context.Marks
                .Include(m => m.Enrollment)
                .Include(m => m.Enrollment!.Subject)
                .FirstOrDefaultAsync(m => m.Id == id && m.Enrollment!.Subject!.UserId == userId);
            if(dbMark is null)
            {
                throw new NotFoundException("Mark not found");
            }
            _context.Marks.Remove(dbMark);
            await _context.SaveChangesAsync();
            return true;
        }

        // user with role teacher can get marks in only his subjects
        // user with role student can get marks in only his enrollments
        public async Task<List<Mark>> GetMarksByEnrollmentId(int id)
        {
            var userId = _userContext.GetUserId();
            var userRole = _userContext.GetUserRole();
            var dbMarks = new List<Mark>();
            if(userRole == AvailableRoles.Teacher)
            {
                dbMarks = await _context.Marks
                    .Include(m => m.Enrollment)
                    .Include(m => m.Enrollment!.User)
                    .Include(m => m.Enrollment!.User!.Role)
                    .Include(m => m.Enrollment!.Subject)
                    .Include(m => m.Enrollment!.Subject!.User)
                    .Include(m => m.Enrollment!.Subject!.User!.Role)
                    .Include(m => m.Enrollment!.Subject!.Semester)
                    .Where(m => m.Enrollment!.Id == id && m.Enrollment!.Subject!.UserId == userId)
                    .ToListAsync();
            }
            else if(userRole == AvailableRoles.Student)
            {
                dbMarks = await _context.Marks
                    .Include(m => m.Enrollment)
                    .Include(m => m.Enrollment!.User)
                    .Include(m => m.Enrollment!.User!.Role)
                    .Include(m => m.Enrollment!.Subject)
                    .Include(m => m.Enrollment!.Subject!.User)
                    .Include(m => m.Enrollment!.Subject!.User!.Role)
                    .Include(m => m.Enrollment!.Subject!.Semester)
                    .Where(m => m.Enrollment!.Id == id && m.Enrollment!.UserId == userId)
                    .ToListAsync();
            }
            if(dbMarks is null)
            {
                throw new NotFoundException("Marks not found");
            }
            return dbMarks;
        }

        // user with role teacher can get marks in only his subjects
        // user with role student can get marks in only his enrollments
        public async Task<List<Mark>> GetMarksBySubjectId(int id)
        {
            var userId = _userContext.GetUserId();
            var userRole = _userContext.GetUserRole();
            var dbMarks = new List<Mark>();
            if(userRole == AvailableRoles.Teacher)
            {
                dbMarks = await _context.Marks
                    .Include(m => m.Enrollment)
                    .Include(m => m.Enrollment!.User)
                    .Include(m => m.Enrollment!.User!.Role)
                    .Include(m => m.Enrollment!.Subject)
                    .Include(m => m.Enrollment!.Subject!.User)
                    .Include(m => m.Enrollment!.Subject!.User!.Role)
                    .Include(m => m.Enrollment!.Subject!.Semester)
                    .Where(m => m.Enrollment!.SubjectId == id && m.Enrollment!.Subject!.UserId == userId)
                    .ToListAsync();
            }
            else if(userRole == AvailableRoles.Student)
            {
                dbMarks = await _context.Marks
                    .Include(m => m.Enrollment)
                    .Include(m => m.Enrollment!.User)
                    .Include(m => m.Enrollment!.User!.Role)
                    .Include(m => m.Enrollment!.Subject)
                    .Include(m => m.Enrollment!.Subject!.User)
                    .Include(m => m.Enrollment!.Subject!.User!.Role)
                    .Include(m => m.Enrollment!.Subject!.Semester)
                    .Where(m => m.Enrollment!.SubjectId == id && m.Enrollment!.UserId == userId)
                    .ToListAsync();
            }
            if(dbMarks is null)
            {
                throw new NotFoundException("Marks not found");
            }
            return dbMarks;
        }
    }
}