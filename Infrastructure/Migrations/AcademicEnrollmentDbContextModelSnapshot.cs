﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AcademicEnrollmentDbContext))]
    partial class AcademicEnrollmentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.EnrolledCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CreditHours")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SemesterEnrollmentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SemesterEnrollmentId");

                    b.ToTable("EnrolledCourses");
                });

            modelBuilder.Entity("Domain.Entities.SemesterEnrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("MaxCreditHours")
                        .HasColumnType("integer");

                    b.Property<string>("SemesterName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("SemesterEnrollments");
                });

            modelBuilder.Entity("Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StudentIdNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("StudentIdNumber")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domain.Entities.EnrolledCourse", b =>
                {
                    b.HasOne("Domain.Entities.SemesterEnrollment", null)
                        .WithMany("EnrolledCourses")
                        .HasForeignKey("SemesterEnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Entities.SemesterEnrollment", b =>
                {
                    b.Navigation("EnrolledCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
