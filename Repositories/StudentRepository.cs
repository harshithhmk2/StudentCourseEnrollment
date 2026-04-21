
using StudentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Repositories{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentEnrollmentContext _context;

        public StudentRepository(StudentEnrollmentContext context)
        {
            _context = context;
        }

        public List<Student> GetAll()
        {
            return _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course).ToList();
        }

        public Student? GetById(int id)
        {
            return _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course).FirstOrDefault(s => s.StudentId == id);
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
        public string AssignCourse(int studentId, int courseId)
        {
            var exists = _context.Enrollments
                .Any(e => e.StudentId == studentId && e.CourseId == courseId);

            if (exists)
                return "Already assigned";

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            return "Course assigned successfully";
        }
        public List<Student> GetStudentsSortedByName()
        {
            return _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .OrderBy(s => s.StudentName)
                .ToList();
        }
        public bool StudentExists(int id)
        {
            return _context.Students.Any(s => s.StudentId == id);
        }

        public bool EnrollmentExists(int studentId, int courseId)
        {
            return _context.Enrollments
                .Any(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}