using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerData.Enums;

namespace TaskManagerData.Models
{
   public class TaskModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskPriority Priority { get; set; }

        public DateTime DueDate { get; set; }

        public TaskProcStatus Status { get; set; } = TaskProcStatus.New;
    }
}
