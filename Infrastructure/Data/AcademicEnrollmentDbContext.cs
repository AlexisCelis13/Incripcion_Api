using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AcademicEnrollmentDbContext : DbContext
    {
        public AcademicEnrollmentDbContext(DbContextOptions<AcademicEnrollmentDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<SemesterEnrollment> SemesterEnrollments { get; set; }
        public DbSet<EnrolledCourse> EnrolledCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones y restricciones
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            modelBuilder.Entity<Student>().HasIndex(s => s.StudentIdNumber).IsUnique();
            modelBuilder.Entity<Student>().HasIndex(s => s.Email).IsUnique();

            modelBuilder.Entity<SemesterEnrollment>().HasKey(e => e.Id);
            modelBuilder.Entity<SemesterEnrollment>()
                .HasMany(e => e.EnrolledCourses)
                .WithOne()
                .HasForeignKey("SemesterEnrollmentId")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EnrolledCourse>().HasKey(c => c.Id);
        }
    }
} 