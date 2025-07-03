using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            var exists = await _studentRepository.GetByStudentIdNumberOrEmailAsync(student.StudentIdNumber, student.Email);
            if (exists != null)
                throw new InvalidOperationException("StudentIdNumber or Email already exists.");
            await _studentRepository.AddAsync(student);
            return student;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync(int page, int pageSize)
        {
            return await _studentRepository.GetAllAsync(page, pageSize);
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<Student?> UpdateStudentAsync(Guid id, Student student)
        {
            var existing = await _studentRepository.GetByIdAsync(id);
            if (existing == null) return null;
            // Actualizar propiedades
            existing = new Student(id, student.StudentIdNumber, student.FirstName, student.LastName, student.Email);
            await _studentRepository.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return false;
            if (await _studentRepository.HasEnrollmentsAsync(id))
                throw new InvalidOperationException("Cannot delete student with enrollments.");
            await _studentRepository.DeleteAsync(student);
            return true;
        }

        public async Task<bool> HasEnrollmentsAsync(Guid studentId)
        {
            return await _studentRepository.HasEnrollmentsAsync(studentId);
        }

        public async Task<bool> IsStudentIdNumberOrEmailDuplicateAsync(string studentIdNumber, string email)
        {
            var exists = await _studentRepository.GetByStudentIdNumberOrEmailAsync(studentIdNumber, email);
            return exists != null;
        }
    }
} 