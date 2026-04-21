namespace StudentManagement.DTO
{
    /// <summary>
    /// Data Transfer Object for Enrollment response.
    /// Does not include back-reference to Student to avoid circular references.
    /// </summary>
    public class EnrollmentResponse
    {
        public int EnrollmentId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? EnrollDate { get; set; }
        public string? CourseName { get; set; }
    }
}
