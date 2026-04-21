using StudentManagement.Models;
using StudentManagement.DTO;
namespace StudentManagement.Services
{
    public interface IAuthService
    {
        object Register(RegisterReqst req);
        object Login(LoginReqst req);
        object Refresh(string token);
    }
}