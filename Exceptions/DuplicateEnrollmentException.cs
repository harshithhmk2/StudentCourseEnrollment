namespace StudentManagement.Exceptions
{
    public class DuplicateEnrollmentException : Exception
    {
        public DuplicateEnrollmentException(int studentId,int courseId)
        
        : base($"Student with id {studentId} has already enrolled in course having {courseId}") { }
        }
    }

