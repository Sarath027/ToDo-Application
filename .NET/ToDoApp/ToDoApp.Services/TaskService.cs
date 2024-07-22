using ToDoApp.DTO;
using ToDoApp.DTO.ViewModel;
using ToDoApp.Repository.Interfaces;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services
{
    public class TaskService : ITodoServiceInterface
    {
        private readonly ITodoRepositoryInterface _todoRepository;
        public TaskService(ITodoRepositoryInterface todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public List<TaskViewModel> GetAllTasks(string userId)
        {
            return ObjectMapper.MapToUserTasksDtoList(_todoRepository.GetAllTasks(userId));
        }


        public void AddTask(TaskDto task, string userId)
        {
            _todoRepository.AddTask(ObjectMapper.MapToModel(task), userId);
        }

        public void EditTask(TaskDto task, string userId)
        {
            _todoRepository.EditTask(ObjectMapper.MapToModel(task), userId);
        }

        public List<TaskViewModel> DeleteTask(int taskId, string userId)
        {
            return ObjectMapper.MapToUserTasksDtoList(_todoRepository.DeleteTask(taskId, userId));
        }

        public List<TaskViewModel> GetActiveTasks(string userId)
        {
            return ObjectMapper.MapToUserTasksDtoList(_todoRepository.GetActiveTasks(userId));
        }

        public List<TaskViewModel> GetCompletedTasks(string userId)
        {
            return ObjectMapper.MapToUserTasksDtoList(_todoRepository.GetCompletedTasks(userId));
        }

        public List<TaskViewModel> ToggleTaskStatus(int taskId, string userId)
        {
            return ObjectMapper.MapToUserTasksDtoList(_todoRepository.ToggleTaskStatus(taskId, userId));
        }

        public List<TaskViewModel> DeleteAllTasks(string userId)
        {
            return ObjectMapper.MapToUserTasksDtoList(_todoRepository.DeleteAllTasks(userId));
        }

        public TaskPercentages GetTaskStatus(string userId)
        {
            return ObjectMapper.MapToTaskPercent(_todoRepository.GetTaskStatus(userId));
        }
    }
}
