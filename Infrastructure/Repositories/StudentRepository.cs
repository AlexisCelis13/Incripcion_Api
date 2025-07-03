using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AcademicEnrollmentDbContext _context;
        public StudentRepository(AcademicEnrollmentDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Students
                .OrderBy(s => s.LastName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Student?> GetByStudentIdNumberOrEmailAsync(string studentIdNumber, string email)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.StudentIdNumber == studentIdNumber || s.Email == email);
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasEnrollmentsAsync(Guid studentId)
        {
            return await _context.SemesterEnrollments.AnyAsync(e => e.StudentId == studentId);
        }
    }
} 