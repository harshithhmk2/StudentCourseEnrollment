using StudentManagement.Exceptions;
using StudentManagement.Models;
using StudentManagement.DTO;
using StudentManagement.Repositories;
using Microsoft.Extensions.Logging;

namespace StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository repo, ILogger<StudentService> logger)
        {
            _repo = repo;
          
            _logger = logger;
        }

        public List<Student> GetAll() => _repo.GetAll();

        public Student GetById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid student ID: {Id}", id);
                throw new ArgumentException("Invalid id");
            }
            var s = _repo.GetById(id);
            if (s == null)
            {
                _logger.LogWarning("Student not found: {Id}", id);
                throw new StudentNotFoundException(id);
            }
            return s;
        }

        public int Add(StudentCreateDto dto)
        {
            var student = new Student
            {
                StudentName = dto.StudentName,
                City = dto.CityName,
                Email= dto.Email
            };

            _repo.Add(student);
            return student.StudentId;
        }

        public void Update(int id, StudentUpdateDto dto)
        {
            var student = _repo.GetById(id);

            if (student == null)
                throw new StudentNotFoundException(id);

            student.StudentName = dto.Name;
            student.City = dto.City;
            student.Email = dto.Email;

            _repo.Update(student);
        }

        public void Delete(int id)
        {
            GetById(id);
            _repo.Delete(id);
        }
        public void AssignCourse(int studentId, int courseId)
        {
            if (!_repo.StudentExists(studentId))
                throw new StudentNotFoundException(studentId);

            if (_repo.EnrollmentExists(studentId, courseId))
                throw new DuplicateEnrollmentException(studentId,courseId);

            _repo.AssignCourse(studentId, courseId);
        }
        public List<StudentResponse> GetStudentsSortedByName()
        {
            return _repo.GetStudentsSortedByName()
                .Select(s => MapToStudentResponse(s))
                .ToList();
        }

        public List<StudentResponse> GetAllAsResponse()
        {
            return _repo.GetAll()
                .Select(s => MapToStudentResponse(s))
                .ToList();
        }

        public StudentResponse? GetByIdAsResponse(int id)
        {
            var student = GetById(id);
            return MapToStudentResponse(student);
        }

        private StudentResponse MapToStudentResponse(Student student)
        {
            return new StudentResponse
            {
                Id = student.StudentId,
                Name = student.StudentName,
                City = student.City ?? "",
                Email = student.Email,
                CreatedDate = student.CreatedDate,
                Enrollments = student.Enrollments?
                    .Select(e => new EnrollmentResponse
                    {
                        EnrollmentId = e.EnrollmentId,
                        CourseId = e.CourseId,
                        EnrollDate = e.EnrollDate,
                        CourseName = e.Course?.CourseName
                    })
                    .ToList()
            };
        }
    }
}
