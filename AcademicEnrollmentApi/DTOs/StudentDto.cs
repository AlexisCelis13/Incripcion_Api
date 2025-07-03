namespace AcademicEnrollmentApi.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string StudentIdNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
} 