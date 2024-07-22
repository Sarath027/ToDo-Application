namespace ToDoApp.DTO.ViewModel
{
    public class TaskViewModel
    {
        public int? TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }

    }
}
