using System.Collections.Generic;

namespace AcademicEnrollmentApi.DTOs
{
    public class SemesterEnrollmentDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string SemesterName { get; set; } = string.Empty;
        public int MaxCreditHours { get; set; }
        public List<EnrolledCourseDto> EnrolledCourses { get; set; } = new();
    }

    public class EnrolledCourseDto
    {
        public Guid Id { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public int CreditHours { get; set; }
    }
} 