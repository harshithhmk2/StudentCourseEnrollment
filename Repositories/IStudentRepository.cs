using StudentManagement.Models;

namespace StudentManagement.Repositories {
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student? GetById(int id);

        void Add(Student student);
        void Update(Student student);
        void Delete(int id);
        string AssignCourse(int StudentId, int courseId);
        List<Student> GetStudentsSortedByName();
        bool StudentExists(int id);
        bool EnrollmentExists(int studentId, int courseId);
    }
}