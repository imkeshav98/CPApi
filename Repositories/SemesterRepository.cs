using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly DataContext _context;
        private readonly IUserContext _userContext;

        public SemesterRepository(DataContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<List<Semester>> GetAllSemesters()
        {
            var dbSemesters = await _context.Semesters.ToListAsync();
            return dbSemesters;
        }

        public async Task<Semester> GetSemesterById(int id)
        {
            var dbSemester = await _context.Semesters.FirstOrDefaultAsync(s => s.Id == id);
            if(dbSemester is null)
            {
                throw new NotFoundException("Semester not found");
            }
            return dbSemester;
        }
        public async Task<Semester> AddSemester(Semester semester)
        {
            _context.Semesters.Add(semester);
            await _context.SaveChangesAsync();
            return semester;
        }

        public async Task<Semester> UpdateSemester(Semester semester)
        {
            var dbSemester = await _context.Semesters.FirstOrDefaultAsync(s => s.Id == semester.Id);
            if(dbSemester is null)
            {
                throw new NotFoundException("Semester not found");
            }

            dbSemester.Name = string.IsNullOrEmpty(semester.Name) ? dbSemester.Name : semester.Name;
            dbSemester.StartDate = semester.StartDate ?? dbSemester.StartDate;
            dbSemester.EndDate = semester.EndDate ?? dbSemester.EndDate;

            await _context.SaveChangesAsync();
            return dbSemester;
        }

        public async Task<bool> CloseSemester(int id)
        {
            var dbSemester = await _context.Semesters.FirstOrDefaultAsync(s => s.Id == id);
            if(dbSemester is null)
            {
                throw new NotFoundException("Semester not found");
            }
            dbSemester.EndDate = DateTime.Now;
            dbSemester.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}