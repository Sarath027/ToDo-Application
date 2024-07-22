using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ToDoApp.Repository.Interfaces;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services
{
    public class UserService : IUserServiceInterface
    {
        private readonly IUserRepositoryInterface _userRepositoryInterface;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepositoryInterface userRepositoryInterface, IConfiguration configuration)
        {
            _userRepositoryInterface = userRepositoryInterface;
            _configuration = configuration;
        }

        public bool ValidateUser(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            return _userRepositoryInterface.ValidateUser(username, hashedPassword);
        }

        public string GenerateJwtToken(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            var userId = _userRepositoryInterface.GetUserId(username, hashedPassword).ToString();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = _configuration["Jwt:Issuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", userId),
                    new Claim("userName", username)
                }),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetUserId(HttpContext context)
        {
            var userIdClaim = context.User.FindFirst(x => x.Type == "userId");
            var userNameClaim = context.User.FindFirst(x => x.Type == "userName");
            return userIdClaim.Value;
        }

        public Boolean SignUp(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            return _userRepositoryInterface.SignUp(username, hashedPassword);
        }

        public string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);

        }
    }
}
