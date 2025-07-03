using Bogus;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class SeedData
    {
        public static async Task SeedAsync(AcademicEnrollmentDbContext context)
        {
            if (await context.Students.AnyAsync()) return; // No repitas si ya hay datos

            var students = new List<Student>();
            var enrollments = new List<SemesterEnrollment>();
            var courses = new List<EnrolledCourse>();

            var studentFaker = new Faker<Student>()
                .CustomInstantiator(f => new Student(
                    Guid.NewGuid(),
                    f.Random.Replace("SID####"),
                    f.Name.FirstName(),
                    f.Name.LastName(),
                    f.Internet.Email()
                ));

            students.AddRange(studentFaker.Generate(100));
            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();

            var enrollmentFaker = new Faker<SemesterEnrollment>()
                .CustomInstantiator(f => new SemesterEnrollment(
                    Guid.NewGuid(),
                    f.PickRandom(students).Id,
                    f.PickRandom(new[] { "2024A", "2024B", "2025A" }),
                    f.Random.Int(20, 40)
                ));

            enrollments.AddRange(enrollmentFaker.Generate(100));
            await context.SemesterEnrollments.AddRangeAsync(enrollments);
            await context.SaveChangesAsync();

            var courseNames = new[] { "Matemáticas I", "Física I", "Química", "Historia", "Literatura", "Programación", "Biología", "Economía", "Arte", "Inglés" };
            var courseCodes = new[] { "MAT101", "FIS101", "QUI101", "HIS101", "LIT101", "PRO101", "BIO101", "ECO101", "ART101", "ING101" };

            var courseFaker = new Faker<EnrolledCourse>()
                .CustomInstantiator(f => new EnrolledCourse(
                    Guid.NewGuid(),
                    f.PickRandom(courseCodes) + f.Random.Number(1, 99),
                    f.PickRandom(courseNames),
                    f.Random.Int(3, 6)
                ));

            // Relacionar cada curso con una inscripción
            for (int i = 0; i < 100; i++)
            {
                var course = courseFaker.Generate();
                // Asignar a una inscripción existente
                context.Entry(course).Property("SemesterEnrollmentId").CurrentValue = enrollments[i % enrollments.Count].Id;
                courses.Add(course);
            }
            await context.EnrolledCourses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
} 