using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTO;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.Controllers
{
    [Authorize]
    [ApiController]                 
    [Route("students")]             
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [Authorize(Policy = "ITOnly")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _service.GetAllAsResponse();
            if (!list.Any())
                return NoContent();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _service.GetByIdAsResponse(id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Post(StudentCreateDto dto)
        {
            var id = _service.Add(dto);

            return CreatedAtAction(nameof(Get), new { id },null);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, StudentUpdateDto dto)
        {
            _service.Update(id, dto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("assign-course")]
        public IActionResult AssignCourse(int studentId, int courseId)
        {
            _service.AssignCourse(studentId, courseId);
            return Ok(new { message = "Course assigned successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Sort")]
        public IActionResult GetSortedStudents()
        {
            return Ok(_service.GetStudentsSortedByName());
        }
    }
}