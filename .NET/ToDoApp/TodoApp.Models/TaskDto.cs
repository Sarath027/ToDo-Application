using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DTO
{
    public class TaskDto
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; } = null!;

        public string Description { get; set; }
    }
}
