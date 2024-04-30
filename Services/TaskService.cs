using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_tracker_group.Models;
using task_tracker_group.Models.Task;

namespace task_tracker_group.Services
{
    public class TaskService
    {

        public bool TaskCreate(TaskCreateDTO task){

            TaskModel taskModel = new ()
            {
                TaskName = task.TaskName,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                UserID = task.UserID,
            };

            return true;
        }
    }
}