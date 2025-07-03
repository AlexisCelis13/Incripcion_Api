using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AcademicEnrollmentDbContext _context;
        public EnrollmentRepository(AcademicEnrollmentDbContext context)
        {
            _context = context;
        }

        public async Task<SemesterEnrollment?> GetByIdAsync(Guid id)
        {
            return await _context.SemesterEnrollments
                .Include(e => e.EnrolledCourses)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<SemesterEnrollment>> GetByStudentIdAsync(Guid studentId)
        {
            return await _context.SemesterEnrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.EnrolledCourses)
                .ToListAsync();
        }

        public async Task AddAsync(SemesterEnrollment enrollment)
        {
            await _context.SemesterEnrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SemesterEnrollment enrollment)
        {
            _context.SemesterEnrollments.Update(enrollment);
            await _context.SaveChangesAsync();
        }
    }
} 