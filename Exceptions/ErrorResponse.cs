namespace StudentManagement.Exceptions
{
    public class ErrorResponse
    {
        public string Message { get; set; } = "";
        public int StatusCode { get; set; }
        public DateTime Time { get; set; } = DateTime.UtcNow;
    }
}
