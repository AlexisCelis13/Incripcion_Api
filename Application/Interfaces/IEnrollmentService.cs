using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task<SemesterEnrollment> CreateEnrollmentAsync(Guid studentId, string semesterName, int maxCreditHours);
        Task<SemesterEnrollment?> GetEnrollmentByIdAsync(Guid semesterId);
        Task<IEnumerable<SemesterEnrollment>> GetEnrollmentsByStudentIdAsync(Guid studentId);
        Task<SemesterEnrollment?> AddCourseToEnrollmentAsync(Guid semesterId, EnrolledCourse course);
    }
} 