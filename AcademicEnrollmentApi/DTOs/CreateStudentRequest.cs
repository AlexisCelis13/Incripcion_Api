namespace AcademicEnrollmentApi.DTOs
{
    public class CreateStudentRequest
    {
        public string StudentIdNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
} 