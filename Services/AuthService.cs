using BCrypt.Net;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;

using StudentManagement.DTO;
using StudentManagement.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Exceptions;

namespace StudentManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly StudentEnrollmentContext _context;

        public AuthService(StudentEnrollmentContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public object Register(RegisterReqst req)
        {
                 if (_context.Users.Any(x => x.Username == req.Username))
           { throw new UserAlreadyExistsException("User exists"); }

            var user = new User
            {
                Username = req.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(req.Password),
                Role = req.Role,
                Department = req.Department
            };

               _context.Users.Add(user);
            _context.SaveChanges();
            return "user registered succesfully";
        }

        public object Login(LoginReqst user)
        {
               var existing = _context.Users.FirstOrDefault(x => x.Username == user.Username);

            if (existing == null || !BCrypt.Net.BCrypt.Verify(user.Password, existing.Password))
                throw new Exception("Invalid credentials");

            var accessToken = GenerateAccessToken(existing);
            var refreshToken = GenerateRefreshToken();

            existing.RefreshToken = refreshToken;
            existing.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            return new
            {
                accessToken,
                refreshToken
            };
        }

        public object Refresh(string token)
        {
            var user = _context.Users.FirstOrDefault(x => x.RefreshToken == token);

            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            var newAccess = GenerateAccessToken(user);
            var newRefresh = GenerateRefreshToken();

            user.RefreshToken = newRefresh;

            return new
            {
                accessToken = newAccess,
                refreshToken = newRefresh
            };
        }

        private string GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Department", user.Department)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}