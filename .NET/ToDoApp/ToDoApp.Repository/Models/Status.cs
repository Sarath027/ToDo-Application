using System;
using System.Collections.Generic;

namespace ToDoApp.Repository.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
}
