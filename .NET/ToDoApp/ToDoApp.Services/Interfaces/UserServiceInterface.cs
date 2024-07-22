using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Services.Interfaces
{
    public interface IUserServiceInterface
    {
        public bool ValidateUser(string username, string password);

        public string GenerateJwtToken(string username, string password);

        public Boolean SignUp(string username, string password);

        public string GetUserId(HttpContext context);

        public string HashPassword(string password);
    }
}
