using StudentManagement.Models;

namespace StudentManagement.DTO
{
    /// <summary>
    /// Data Transfer Object for Student response.
    /// Contains student information with enrollments without circular references.
    /// </summary>
    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string City { get; set; } = "";
        public string? Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<EnrollmentResponse>? Enrollments { get; set; }
    }
}
