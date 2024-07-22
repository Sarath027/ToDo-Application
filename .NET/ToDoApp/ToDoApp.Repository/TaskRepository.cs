using Microsoft.EntityFrameworkCore;
using ToDoApp.Repository.Interfaces;
using ToDoApp.Repository.Models;
using ToDoApp.Repository.Enums;
using ToDoApp.Repository.Dto;

namespace ToDoApp.Repository
{
    public class TaskRepository: ITodoRepositoryInterface
    {
        private readonly ToDoAppContext _todoContext;

        public TaskRepository(ToDoAppContext todoContext)
        {
            _todoContext = todoContext;
        }

        public IEnumerable<UserTask> GetAllTasks(string userId)
        {
            var tasks = _todoContext.UserTasks.Include(task => task.Status)
                                              .Include(task => task.Task)
                                              .Include(task => task.User).Where(task => task.UserId == int.Parse(userId));
            return tasks;
        }

        public void AddTask(TaskInfo task, string userId)
        {
            if (!_todoContext.TaskInfos.Where(t => t.TaskName.Equals(task.TaskName) && t.Description.Equals(task.Description)).Any())
            {
                _todoContext.TaskInfos.Add(task);
                _todoContext.SaveChanges();
            }
            UserTask userTask = new UserTask();
            if (task.Description != null)
            {
                userTask.TaskId = GetTaskId(task.TaskName, task.Description);
            }
            userTask.StatusId = (int) StatusEnum.Active;
            userTask.UserId = int.Parse(userId);
            userTask.CreatedOn = DateTime.Now;
            _todoContext.UserTasks.Add(userTask);
            _todoContext.SaveChanges();
            
        }

        public int GetTaskId(string title, string description)
        {
            var taskId = _todoContext.TaskInfos.Where(task =>
            task.TaskName.Equals(title) && task.Description.Equals(description))
                .Select(task=>task.TaskId).FirstOrDefault();
            return taskId;
        }


        public void EditTask(TaskInfo task, string userId)
        {
            var editedTask = new TaskInfo
            {
                TaskName = task.TaskName,
                Description = task.Description,
            };
            _todoContext.TaskInfos.Add(editedTask);
            _todoContext.SaveChanges();
            var taskId = GetTaskId(task.TaskName, task.Description);
            var tasks = _todoContext.UserTasks.FirstOrDefault(userTask=>userTask.TaskId==task.TaskId && userTask.UserId==int.Parse(userId));
            if (tasks != null)
            {
                tasks.TaskId = taskId;
            }
            _todoContext.SaveChanges();
        }


        public IEnumerable<UserTask> DeleteTask(int taskId, string userId)
        {
            var task = _todoContext.TaskInfos.Where(task=>task.TaskId == taskId).FirstOrDefault();
            var userTask = _todoContext.UserTasks.Where(userTask => userTask.UserId==int.Parse(userId) && userTask.TaskId.Equals(taskId)).FirstOrDefault();
            if (userTask != null)
            {
                _todoContext.UserTasks.Remove(userTask);
            }
            _todoContext.SaveChanges();
            if (userTask.StatusId == (int)StatusEnum.Active)
            {
                return GetActiveTasks(userId);
            }
            else
            {
                return GetCompletedTasks(userId);
            }
        }


        public IEnumerable<UserTask> GetActiveTasks(string userId)
        {
            var tasks = _todoContext.UserTasks.Include(task => task.Status)
                                              .Include(task => task.Task)
                                              .Include(task => task.User).Where(task => task.UserId == int.Parse(userId));
            var activeTasks = tasks.Where(task => task.Status.StatusId == (int) StatusEnum.Active);
            return activeTasks;
        }


        public IEnumerable<UserTask> GetCompletedTasks(string userId)
        {
            var tasks = _todoContext.UserTasks.Include(task => task.Status)
                                              .Include(task => task.Task)
                                              .Include(task => task.User).Where(task => task.UserId == int.Parse(userId));
            var completedTasks = tasks.Where(task => task.Status.StatusId == (int) StatusEnum.Completed);
            return completedTasks;
        }

        public IEnumerable<UserTask>? ToggleTaskStatus(int taskId, string userId)
        {
            var Id = int.Parse(userId);
            var userTasks = _todoContext.UserTasks.Where(task => task.UserId==Id && task.TaskId==taskId).FirstOrDefault();
            
            if(userTasks != null)
            {
                if (userTasks.StatusId == (int) StatusEnum.Active)
                {
                    userTasks.CompletedOn = DateTime.Now;
                    userTasks.StatusId = (int) StatusEnum.Completed;
                    _todoContext.SaveChanges();
                    return GetActiveTasks(userId);
                }
                else
                {
                    userTasks.StatusId = (int)StatusEnum.Active;
                    _todoContext.SaveChanges();
                    return GetCompletedTasks(userId);
                }
            }
            return null;
        }


        public IEnumerable<UserTask> DeleteAllTasks(string userId)
        {
            var userTasks = _todoContext.UserTasks.Where(task => task.UserId == int.Parse(userId));
            foreach(var task in userTasks)
            {
                _todoContext.UserTasks.Remove(task);
            }
            _todoContext.SaveChanges();
            return GetAllTasks(userId);
        }

        public TaskPercentageDto GetTaskStatus(string userId)
        {
            var Status = new List<int>();
            var activeTasksCount = _todoContext.UserTasks.Where(task =>task.UserId==int.Parse(userId) && task.StatusId == (int) StatusEnum.Active).Count();
            var completedTasksCount = _todoContext.UserTasks.Where(task => task.UserId == int.Parse(userId) && task.StatusId== (int)StatusEnum.Completed).Count();
            var percentages = new TaskPercentageDto
            {
                ActivePercent = 100 * activeTasksCount / (activeTasksCount + completedTasksCount),
                CompletedPercent = 100 * completedTasksCount / (activeTasksCount + completedTasksCount)
            };
            return percentages;
        }
    }
}
