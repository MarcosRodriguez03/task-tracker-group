using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace task_tracker_group.Models.Task
{
    public class TaskModel
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "To-Do";
        public int Priority { get; set; } = 1;
        public string? Assignee { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserID { get; set; }

    public TaskModel()
    {
        DateCreated = DateTime.Now;
    }

    
    }

}