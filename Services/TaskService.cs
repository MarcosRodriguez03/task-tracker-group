using Microsoft.Extensions.ObjectPool;
using task_tracker_group.Models;
using task_tracker_group.Models.DTO;
using task_tracker_group.Services.Context;

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

            bool result = false;

            TaskModel taskModel = new ()
            {
                ID = task.ID,
                TaskName = task.TaskName,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                UserID = task.UserID,
                BoardID = task.BoardID,
            };

            _context.Add(taskModel);

            return _context.SaveChanges() != 0;
        }

        public bool AddComment(CommentDTO comment)
        {

            
            CommentModel commentModel = new ()
            {
                ID = comment.ID,
                Comment = comment.Comment,
                Username = comment.Username,
                UserID = comment.UserID,
            };

            _context.Add(commentModel);

            return _context.SaveChanges() != 0;
        }

        public bool EditTask(TaskCreateDTO task)
        {

            bool result = false;

            TaskModel foundTask = GetTask(task.ID);

            if(foundTask != null)
            {
                foundTask.Description = task.Description;
                foundTask.Priority = task.Priority;
                foundTask.Status = task.Status;
                _context.Update<TaskModel>(foundTask);
                result = _context.SaveChanges() != 0;
            };
            
            return result;
        }


        public TaskModel GetTask(int id)
        {
            return _context.TaskInfo.FirstOrDefault(task => task.ID == id);
        }


        public List<TaskModel> GetBoardTasks(string boardID)
        {

            var properties = _context.TaskInfo.Where(task => task.BoardID == boardID).ToList();

            return properties;
        }

    }
}