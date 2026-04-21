namespace StudentManagement.DTO{
    public class RegisterReqst
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "User";
        public string Department { get; set; } = "General";
    }
}