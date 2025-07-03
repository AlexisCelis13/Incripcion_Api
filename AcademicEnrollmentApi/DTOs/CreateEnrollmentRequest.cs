namespace AcademicEnrollmentApi.DTOs
{
    public class CreateEnrollmentRequest
    {
        public Guid StudentId { get; set; }
        public string SemesterName { get; set; } = string.Empty;
        public int MaxCreditHours { get; set; }
    }
} 