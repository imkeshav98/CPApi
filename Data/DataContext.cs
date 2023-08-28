using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Semester> Semesters => Set<Semester>(); 
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Mark> Marks => Set<Mark>();
    }
}