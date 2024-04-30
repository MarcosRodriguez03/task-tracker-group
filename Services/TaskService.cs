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

        private readonly DataContext _context;
        public TaskService(DataContext context)
        {
            _context = context;
        }

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

        public bool AddComment(CommentDTO comment)
        {
            CommentModel commentModel = new ()
            {
                Comment = comment.Comment,
                UserID = comment.UserID,
            }

            return true;
        }

        public bool EditTask(TaskEditDTO task)
        {
            return true;
        }


        public TaskModel GetTask(int id)
        {
            return _context.TaskInfo.SingleOrDefault(task => task.ID == id) != null;
        }
    }
}