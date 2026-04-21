using StudentManagement.DTO;
using StudentManagement.Models;

public interface IStudentService
{
    List<Student> GetAll();
    Student GetById(int id);

    int Add(StudentCreateDto dto);
    void Update(int id, StudentUpdateDto dto);

    void Delete(int id);
    void AssignCourse(int studentId, int courseId);
    List<StudentResponse> GetStudentsSortedByName();
    List<StudentResponse> GetAllAsResponse();
    StudentResponse? GetByIdAsResponse(int id);
}

//# Run all tests
//dotnet test

//# Run specific test class
//dotnet test --filter ClassName=StudentManagement.Tests.Services.StudentServiceTests

//# Run with details
//dotnet test --verbosity detailed