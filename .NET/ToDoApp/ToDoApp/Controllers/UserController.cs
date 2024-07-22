using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTO;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServiceInterface _userService;
        public UserController(IUserServiceInterface userService)
        {
            _userService = userService;
        }


        [HttpPost("Login")]
        public IActionResult Login(User user)
        {
            if (_userService.ValidateUser(user.UserName, user.Password))
            {
                var token = _userService.GenerateJwtToken(user.UserName, user.Password);
                return Ok(token);
            }
            else
            {
                return BadRequest("Invalid login credentials");
            }
        }

        [HttpPost("SignUp")]
        public Boolean SignUp(User user)
        {
            return _userService.SignUp(user.UserName, user.Password);
        }
    }
}
