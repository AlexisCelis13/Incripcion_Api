namespace Domain.Entities
{
    public class EnrolledCourse
    {
        public Guid Id { get; private set; }
        public string CourseCode { get; private set; }
        public string CourseName { get; private set; }
        public int CreditHours { get; private set; }

        public EnrolledCourse(Guid id, string courseCode, string courseName, int creditHours)
        {
            Id = id;
            CourseCode = courseCode;
            CourseName = courseName;
            CreditHours = creditHours;
        }
    }
} 