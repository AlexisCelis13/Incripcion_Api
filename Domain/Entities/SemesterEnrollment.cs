using Domain.Exceptions;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class SemesterEnrollment
    {
        public Guid Id { get; private set; }
        public Guid StudentId { get; private set; }
        public string SemesterName { get; private set; }
        public int MaxCreditHours { get; private set; }

        public List<EnrolledCourse> EnrolledCourses { get; private set; } = new();

        public SemesterEnrollment(Guid id, Guid studentId, string semesterName, int maxCreditHours)
        {
            Id = id;
            StudentId = studentId;
            SemesterName = semesterName;
            MaxCreditHours = maxCreditHours;
        }

        public void AddCourse(EnrolledCourse course)
        {
            int totalCredits = EnrolledCourses.Sum(c => c.CreditHours);
            if (totalCredits + course.CreditHours > MaxCreditHours)
                throw new CreditLimitExceededException($"Credit limit of {MaxCreditHours} exceeded.");
            EnrolledCourses.Add(course);
        }

        public IReadOnlyCollection<EnrolledCourse> GetEnrolledCourses() => EnrolledCourses.AsReadOnly();
    }
} 