using AcademicEnrollmentApi.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademicEnrollmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
        {
            if (await _studentService.IsStudentIdNumberOrEmailDuplicateAsync(request.StudentIdNumber, request.Email))
                return BadRequest("StudentIdNumber or Email already exists.");
            var student = new Student(Guid.NewGuid(), request.StudentIdNumber, request.FirstName, request.LastName, request.Email);
            var created = await _studentService.CreateStudentAsync(student);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new StudentDto
            {
                Id = created.Id,
                StudentIdNumber = created.StudentIdNumber,
                FirstName = created.FirstName,
                LastName = created.LastName,
                Email = created.Email
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var students = await _studentService.GetStudentsAsync(page, pageSize);
            return Ok(students.Select(s => new StudentDto
            {
                Id = s.Id,
                StudentIdNumber = s.StudentIdNumber,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(new StudentDto
            {
                Id = student.Id,
                StudentIdNumber = student.StudentIdNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStudentRequest request)
        {
            var updated = await _studentService.UpdateStudentAsync(id, new Student(id, request.StudentIdNumber, request.FirstName, request.LastName, request.Email));
            if (updated == null) return NotFound();
            return Ok(new StudentDto
            {
                Id = updated.Id,
                StudentIdNumber = updated.StudentIdNumber,
                FirstName = updated.FirstName,
                LastName = updated.LastName,
                Email = updated.Email
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _studentService.DeleteStudentAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
} 