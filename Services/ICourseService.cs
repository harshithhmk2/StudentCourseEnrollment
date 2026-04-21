using StudentManagement.DTO;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public interface ICourseService
    {
        List<Course> GetAll();
        Course GetById(int id);
        int Add(CourseCreateDto dto);
        void Update(int id, CourseUpdateDto dto);
        void Delete(int id);
    }
}
