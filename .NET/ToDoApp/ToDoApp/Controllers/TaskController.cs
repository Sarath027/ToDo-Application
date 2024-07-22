using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTO;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly ITodoServiceInterface _todoService;
        private readonly IUserServiceInterface _userService;


        public TaskController(ITodoServiceInterface todoService,
                              IUserServiceInterface userService)
        {
            _todoService = todoService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            try
            {
                var userId = _userService.GetUserId(HttpContext);
                if (userId == null)
                {
                    return BadRequest("User Id is null");
                }
                var tasks = _todoService.GetAllTasks(userId);
                if (tasks == null)
                {
                    return NotFound("No tasks found");
                }
                return Ok(tasks);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPost("AddTask")]
        public IActionResult AddTask(TaskDto task)
        {
            try
            {
                var userId = _userService.GetUserId(HttpContext);
                if (userId == null)
                {
                    return BadRequest("User Id is null");
                }
                _todoService.AddTask(task, userId);
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut]
        public IActionResult EditTask(TaskDto task)
        {
            try
            {
                var userId = _userService.GetUserId(HttpContext);
                if (userId == null)
                {
                    return BadRequest("User Id is null");
                }
                _todoService.EditTask(task, userId);
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteTask(int taskId)
        {
            try
            {
                var userId = _userService.GetUserId(HttpContext);
                if (userId == null)
                {
                    return BadRequest("User Id is null");
                }
                var tasks = _todoService.DeleteTask(taskId, userId);
                if (tasks == null)
                {
                    return NotFound("No tasks found");
                }
                return Ok(tasks);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }


        [HttpDelete("DeleteAll")]
        public IActionResult DeleteAllTasks()
        {
            try
            {
                var userId = _userService.GetUserId(HttpContext);
                if (userId == null)
                {
                    return BadRequest("User Id is null");
                }
                return Ok(_todoService.DeleteAllTasks(userId));
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }


        [HttpGet("ActiveTasks")]
        public IActionResult GetActiveTasks()
        {
            try
            {
                var userId = _userService.GetUserId(HttpContext);
                if (userId == null)
                {
                    return BadRequest("User Id is null");
                }
                var tasks = _todoService.GetActiveTasks(userId);
                if (tasks == null)
                {
                    return NotFound("No tasks found");
                }
                return Ok(tasks);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("CompletedTasks")]
        public IActionResult GetCompletedTasks()
        {
            try
            {
                var userId = _userService.GetUserId(HttpContext);
                if (userId == null)
                {
                    return BadRequest("User Id is null");
                }
                var tasks = _todoService.GetCompletedTasks(userId);
                if (tasks == null)
                {
                    return NotFound("No tasks found");
                }
                return Ok(tasks);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("ToggleTaskStatus")]
        public IActionResult ToggleTaskStatus(int taskId)
        {
            try
            {
                var userId = _userService.GetUserId(HttpContext);
                if (userId == null)
                {
                    return BadRequest("User Id is null");
                }
                var tasks = _todoService.ToggleTaskStatus(taskId, userId);
                if (tasks == null)
                {
                    return NotFound("No tasks found");
                }
                return Ok(tasks);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("TaskStatus")]
        public IActionResult GetTaskStatus()
        {
            try
            {
                var userId = _userService.GetUserId(HttpContext);
                if (userId == null)
                {
                    return BadRequest("User Id is null");
                }
                var taskPercentages = _todoService.GetTaskStatus(userId);
                if (taskPercentages == null)
                {
                    return NotFound("No tasks found");
                }
                return Ok(taskPercentages);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}
