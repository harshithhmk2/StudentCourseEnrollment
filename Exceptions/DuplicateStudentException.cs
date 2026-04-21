namespace StudentManagement.Exceptions
{
    public class DuplicateStudentException : Exception
    {
        public DuplicateStudentException(int id)
            : base($"Student with id {id} already exists") { }
    }
}
