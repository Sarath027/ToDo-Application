using ToDoApp.Repository.Models;
using ToDoApp.Repository.Dto;

namespace ToDoApp.Repository.Interfaces
{
    public interface ITodoRepositoryInterface
    {
        public IEnumerable<UserTask> GetAllTasks(string userId);

        public void AddTask(TaskInfo task, string userId);

        public int GetTaskId(string title, string description);

        public void EditTask(TaskInfo task, string userId);

        public IEnumerable<UserTask> DeleteTask(int taskId, string userId);

        public IEnumerable<UserTask> GetActiveTasks(string userId);

        public IEnumerable<UserTask> GetCompletedTasks(string userId);

        public IEnumerable<UserTask>? ToggleTaskStatus(int taskId, string userId);

        public IEnumerable<UserTask> DeleteAllTasks(string userId);

        public TaskPercentageDto GetTaskStatus(string userId);
    }
}
