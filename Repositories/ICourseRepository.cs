using StudentManagement.Models;

namespace StudentManagement.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetAll();
        Course? GetById(int id);
        int Add(Course course);
        void Update(int id, Course course);
        void Delete(int id);
        bool CourseExists(int id);
    }
}
