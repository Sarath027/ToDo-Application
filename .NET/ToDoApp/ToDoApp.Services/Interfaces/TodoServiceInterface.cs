using ToDoApp.DTO;
using ToDoApp.DTO.ViewModel;
using ToDoApp.Repository.Models;

namespace ToDoApp.Services.Interfaces
{
    public interface ITodoServiceInterface
    {
        public List<TaskViewModel> GetAllTasks(string userId);

        public void AddTask(TaskDto task, string userId);

        public void EditTask(TaskDto task, string userId);

        public List<TaskViewModel> DeleteTask(int taskId, string userId);

        public List<TaskViewModel> GetActiveTasks(string userId);

        public List<TaskViewModel> GetCompletedTasks(string userId);

        public List<TaskViewModel> ToggleTaskStatus(int taskId, string userId);

        public List<TaskViewModel> DeleteAllTasks(string userId);

        public TaskPercentages GetTaskStatus(string userId);
    }
}
