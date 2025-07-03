using Domain.Entities;

namespace Application.Interfaces
{
    public interface IStudentService
    {
        Task<Student> CreateStudentAsync(Student student);
        Task<IEnumerable<Student>> GetStudentsAsync(int page, int pageSize);
        Task<Student?> GetStudentByIdAsync(Guid id);
        Task<Student?> UpdateStudentAsync(Guid id, Student student);
        Task<bool> DeleteStudentAsync(Guid id);
        Task<bool> HasEnrollmentsAsync(Guid studentId);
        Task<bool> IsStudentIdNumberOrEmailDuplicateAsync(string studentIdNumber, string email);
    }
} 