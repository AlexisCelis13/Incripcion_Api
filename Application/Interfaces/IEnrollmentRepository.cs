using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<SemesterEnrollment?> GetByIdAsync(Guid id);
        Task<IEnumerable<SemesterEnrollment>> GetByStudentIdAsync(Guid studentId);
        Task AddAsync(SemesterEnrollment enrollment);
        Task UpdateAsync(SemesterEnrollment enrollment);
        Task AddCourseAsync(Guid semesterId, Domain.Entities.EnrolledCourse course);
    }
} 