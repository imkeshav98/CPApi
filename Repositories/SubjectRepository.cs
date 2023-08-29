using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;
        private readonly IUserContext _userContext;

        public SubjectRepository(DataContext context , IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            var userId = _userContext.GetUserId();
            var userRole = _userContext.GetUserRole();
            var dbSubjects = new List<Subject>();
            if(userRole == AvailableRoles.Teacher){
                dbSubjects = await _context.Subjects
                .Include(s => s.User)
                .Include(s => s.User!.Role)
                .Include(s => s.Semester)
                .Where(s => s.UserId == userId && s.Status == true)
                .ToListAsync();
            }
            else{
                dbSubjects = await _context.Subjects
                .Include(s => s.User)
                .Include(s => s.User!.Role)
                .Include(s => s.Semester)
                .Where(s => s.Status == true)
                .ToListAsync();
            }
            return dbSubjects;
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            var userId = _userContext.GetUserId();
            var userRole = _userContext.GetUserRole();
            var dbSubject = new Subject();
            if(userRole == AvailableRoles.Teacher){
                dbSubject = await _context.Subjects
                .Include(s => s.User)
                .Include(s => s.User!.Role)
                .Include(s => s.Semester)
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId && s.Status == true);
            }
            else{
                dbSubject = await _context.Subjects
                .Include(s => s.User)
                .Include(s => s.User!.Role)
                .Include(s => s.Semester)
                .FirstOrDefaultAsync(s => s.Id == id && s.Status == true);
            }
            if(dbSubject is null)
            {
                throw new NotFoundException("Subject not found");
            }
            return dbSubject;
        }

        // No need to check for user role here because only teacher can add subject
        public async Task<Subject> AddSubject(Subject subject)
        {
            var userId = _userContext.GetUserId();
            subject.UserId = userId;
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            var dbSubject = await _context.Subjects
                .Include(s => s.User)
                .Include(s => s.User!.Role)
                .Include(s => s.Semester)
                .FirstOrDefaultAsync(s => s.Id == subject.Id && s.Status == true);
            if(dbSubject is null)
            {
                throw new Exception("Error while adding subject");
            }
            return dbSubject;
        }

        // No need to check for user role here because only teacher can update subject
        public async Task<Subject> UpdateSubject(Subject subject)
        {
            var userId = _userContext.GetUserId();
            var dbSubject = await _context.Subjects
                .Include(s => s.User)
                .Include(s => s.User!.Role)
                .Include(s => s.Semester)
                .Where(s => s.UserId == userId && s.Status == true)           
                .FirstOrDefaultAsync(s => s.Id == subject.Id);
            if(dbSubject is null)
            {
                throw new NotFoundException("Subject not found");
            }
            dbSubject.Name = string.IsNullOrEmpty(subject.Name) ? dbSubject.Name : subject.Name;
            dbSubject.Code = string.IsNullOrEmpty(subject.Code) ? dbSubject.Code : subject.Code;
            await _context.SaveChangesAsync();
            return dbSubject;
        }

        // No need to check for user role here because only teacher can delete subject
        public async Task<bool> CloseSubject(int id)
        {
            var dbSubject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id && s.Status == true);
            if(dbSubject is null)
            {
                throw new NotFoundException("Subject not found");
            }
            dbSubject.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Subject>> GetSubjectsBySemId(int id)
        {
            var subjects = await _context.Subjects
            .Include(s => s.User)
            .Include(s => s.User!.Role)
            .Include(s => s.Semester)
            .Where(s => s.Semester!.Id == id)
            .ToListAsync();

            if(subjects is null)
            {
                throw new NotFoundException("Subjects not found");
            }
            return subjects;
        }

    }
}