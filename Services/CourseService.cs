using StudentManagement.DTO;
using StudentManagement.Models;
using StudentManagement.Repositories;
using Microsoft.Extensions.Logging;

namespace StudentManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository repo, ILogger<CourseService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public List<Course> GetAll()
        {
            return _repo.GetAll();
        }

        public Course GetById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid course ID: {Id}", id);
                throw new ArgumentException("Invalid id");
            }

            var course = _repo.GetById(id);
            if (course == null)
            {
                _logger.LogWarning("Course not found: {Id}", id);
                throw new KeyNotFoundException($"Course with ID {id} not found");
            }

            return course;
        }

        public int Add(CourseCreateDto dto)
        {
            var course = new Course
            {
                CourseName = dto.CourseName,
                Credits = dto.Credits
            };

            return _repo.Add(course);
        }

        public void Update(int id, CourseUpdateDto dto)
        {
            if (!_repo.CourseExists(id))
            {
                _logger.LogWarning("Course not found: {Id}", id);
                throw new KeyNotFoundException($"Course with ID {id} not found");
            }

            var course = new Course
            {
                CourseId = id,
                CourseName = dto.CourseName,
                Credits = dto.Credits
            };

            _repo.Update(id, course);
        }

        public void Delete(int id)
        {
            GetById(id);
            _repo.Delete(id);
        }
    }
}
