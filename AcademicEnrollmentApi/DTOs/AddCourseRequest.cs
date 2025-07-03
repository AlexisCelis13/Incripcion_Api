namespace AcademicEnrollmentApi.DTOs
{
    public class AddCourseRequest
    {
        public string CourseCode { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public int CreditHours { get; set; }
    }
} 