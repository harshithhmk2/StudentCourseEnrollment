using StudentManagement.Models;

namespace StudentManagement.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StudentEnrollmentContext _context;

        public CourseRepository(StudentEnrollmentContext context)
        {
            _context = context;
        }

        public List<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course? GetById(int id)
        {
            return _context.Courses.FirstOrDefault(c => c.CourseId == id);
        }

        public int Add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course.CourseId;
        }

        public void Update(int id, Course course)
        {
            var existingCourse = _context.Courses.Find(id);
            if (existingCourse != null)
            {
                existingCourse.CourseName = course.CourseName;
                existingCourse.Credits = course.Credits;
                _context.Courses.Update(existingCourse);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }

        public bool CourseExists(int id)
        {
            return _context.Courses.Any(c => c.CourseId == id);
        }
    }
}
