using AcademicEnrollmentApi.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademicEnrollmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnrollmentRequest request)
        {
            try
            {
                var enrollment = await _enrollmentService.CreateEnrollmentAsync(request.StudentId, request.SemesterName, request.MaxCreditHours);
                return Ok(new SemesterEnrollmentDto
                {
                    Id = enrollment.Id,
                    StudentId = enrollment.StudentId,
                    SemesterName = enrollment.SemesterName,
                    MaxCreditHours = enrollment.MaxCreditHours,
                    EnrolledCourses = enrollment.EnrolledCourses.Select(c => new EnrolledCourseDto
                    {
                        Id = c.Id,
                        CourseCode = c.CourseCode,
                        CourseName = c.CourseName,
                        CreditHours = c.CreditHours
                    }).ToList()
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{semesterId}/courses")]
        public async Task<IActionResult> AddCourse(Guid semesterId, [FromBody] AddCourseRequest request)
        {
            try
            {
                var course = new EnrolledCourse(Guid.NewGuid(), request.CourseCode, request.CourseName, request.CreditHours);
                var enrollment = await _enrollmentService.AddCourseToEnrollmentAsync(semesterId, course);
                if (enrollment == null) return NotFound();
                return Ok(new SemesterEnrollmentDto
                {
                    Id = enrollment.Id,
                    StudentId = enrollment.StudentId,
                    SemesterName = enrollment.SemesterName,
                    MaxCreditHours = enrollment.MaxCreditHours,
                    EnrolledCourses = enrollment.EnrolledCourses.Select(c => new EnrolledCourseDto
                    {
                        Id = c.Id,
                        CourseCode = c.CourseCode,
                        CourseName = c.CourseName,
                        CreditHours = c.CreditHours
                    }).ToList()
                });
            }
            catch (CreditLimitExceededException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{semesterId}")]
        public async Task<IActionResult> GetById(Guid semesterId)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(semesterId);
            if (enrollment == null) return NotFound();
            return Ok(new SemesterEnrollmentDto
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                SemesterName = enrollment.SemesterName,
                MaxCreditHours = enrollment.MaxCreditHours,
                EnrolledCourses = enrollment.EnrolledCourses.Select(c => new EnrolledCourseDto
                {
                    Id = c.Id,
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    CreditHours = c.CreditHours
                }).ToList()
            });
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudentId(Guid studentId)
        {
            var enrollments = await _enrollmentService.GetEnrollmentsByStudentIdAsync(studentId);
            return Ok(enrollments.Select(e => new SemesterEnrollmentDto
            {
                Id = e.Id,
                StudentId = e.StudentId,
                SemesterName = e.SemesterName,
                MaxCreditHours = e.MaxCreditHours,
                EnrolledCourses = e.EnrolledCourses.Select(c => new EnrolledCourseDto
                {
                    Id = c.Id,
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    CreditHours = c.CreditHours
                }).ToList()
            }));
        }
    }
} 