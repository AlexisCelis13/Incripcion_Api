using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
        }

        public async Task<SemesterEnrollment> CreateEnrollmentAsync(Guid studentId, string semesterName, int maxCreditHours)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new InvalidOperationException("Student not found.");
            var enrollment = new SemesterEnrollment(Guid.NewGuid(), studentId, semesterName, maxCreditHours);
            await _enrollmentRepository.AddAsync(enrollment);
            return enrollment;
        }

        public async Task<SemesterEnrollment?> GetEnrollmentByIdAsync(Guid semesterId)
        {
            return await _enrollmentRepository.GetByIdAsync(semesterId);
        }

        public async Task<IEnumerable<SemesterEnrollment>> GetEnrollmentsByStudentIdAsync(Guid studentId)
        {
            return await _enrollmentRepository.GetByStudentIdAsync(studentId);
        }

        public async Task<SemesterEnrollment?> AddCourseToEnrollmentAsync(Guid semesterId, EnrolledCourse course)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(semesterId);
            if (enrollment == null) return null;
            try
            {
                enrollment.AddCourse(course);
            }
            catch (CreditLimitExceededException)
            {
                throw;
            }
            await _enrollmentRepository.UpdateAsync(enrollment);
            return enrollment;
        }
    }
} 