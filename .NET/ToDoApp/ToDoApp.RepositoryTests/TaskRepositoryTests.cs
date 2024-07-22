using Microsoft.EntityFrameworkCore;
using Moq;
using ToDoApp.DTO.ViewModel;
using ToDoApp.Repository.Models;

namespace ToDoApp.Repository.Tests
{
    [TestClass]
    public class TaskRepositoryTests
    {
        private readonly TaskRepository _taskRepository;
        private readonly ToDoAppContext _appContext;
        public TaskRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ToDoAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _appContext = new ToDoAppContext(options);
            _taskRepository = new TaskRepository(_appContext);
            LoadData();
        }

        private void LoadData()
        {
            var task = new TaskInfo
            {
                TaskId = 1120,
                TaskName = "Task-1",
                Description = "Task-1 description"
            };
            _appContext.TaskInfos.Add(task);
            _appContext.SaveChanges();
        }

        [TestMethod]
        public void GetTaskId_ReturnTaskId()
        {
            var title = "Task-1";
            var description = "Task-1 description";
            var taskId = _taskRepository.GetTaskId(title, description);
            Assert.AreEqual(1120,taskId);
        }

        [TestMethod]
        public void AddTask_AddTasksToDatabase()
        {
            var task = new TaskInfo
            {
                TaskId = 1121,
                TaskName = "New Task",
                Description = "New Task Description"
            };

            _appContext.TaskInfos.Add(task);
            _appContext.SaveChanges();
        }

    }
}