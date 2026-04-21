using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTO;
using StudentManagement.Models;

using StudentManagement.Services;

namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterReqst user)
        {
            return Ok(_auth.Register(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginReqst user)
        {
            var token = _auth.Login(user);
            return Ok(new { token });
        }
        [HttpPost("refresh")]
        public IActionResult Refresh(RefreshRequest request)
        {
            return Ok(_auth.Refresh(request.Token));
        }
    }
}