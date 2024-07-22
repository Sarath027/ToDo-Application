using ToDoApp.DTO;
using ToDoApp.DTO.ViewModel;
using ToDoApp.Repository.Models;
using ToDoApp.Repository.Dto;


namespace ToDoApp.Services
{
    public class ObjectMapper
    {
        public static TaskInfo MapToModel(TaskDto dto)
        {
            TaskInfo model = new TaskInfo();
            model.TaskId = dto.TaskId;
            model.TaskName = dto.TaskName;
            model.Description = dto.Description;
            return model;
        }

        public static TaskViewModel MaptoUserTaskDto(UserTask userTask)
        {
            TaskViewModel model = new TaskViewModel();
            if (userTask.Status != null)
            {
                model.Status = userTask.Status.Name;
            }
            model.TaskId = userTask.TaskId;
            if (userTask.Task != null)
            {
                model.Title = userTask.Task.TaskName;
                if (userTask.Task.Description != null)
                    model.Description = userTask.Task.Description;
            }

            if (userTask.CompletedOn != null)
            {
                model.CompletedOn = (DateTime)userTask.CompletedOn;
            }
            if (userTask.CreatedOn != null)
            {
                model.CreatedOn = (DateTime)userTask.CreatedOn;
            }

            return model;
        }

        public static List<TaskViewModel> MapToUserTasksDtoList(IEnumerable<UserTask> userTask)
        {
            List<TaskViewModel> taskViewModels = new List<TaskViewModel>();
            foreach (var task in userTask)
            {
                taskViewModels.Add(MaptoUserTaskDto((UserTask)task));
            }
            return taskViewModels;
        }

        public static TaskPercentages MapToTaskPercent(TaskPercentageDto taskPercentagesDto)
        {
            var taskPercentages = new TaskPercentages
            {
                ActivePercent = taskPercentagesDto.ActivePercent,
                CompletedPercent = taskPercentagesDto.CompletedPercent,
            };
            return taskPercentages;
        }


    }
}
