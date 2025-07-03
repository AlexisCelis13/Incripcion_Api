using Domain.Entities;

namespace Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(Guid id);
        Task<IEnumerable<Student>> GetAllAsync(int page, int pageSize);
        Task<Student?> GetByStudentIdNumberOrEmailAsync(string studentIdNumber, string email);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task<bool> HasEnrollmentsAsync(Guid studentId);
    }
} 