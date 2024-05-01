using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using task_tracker_group.Models;
using task_tracker_group.Models.DTO;
using task_tracker_group.Services;

namespace task_tracker_group.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {

        private readonly TaskService _taskService;
        public TaskController(TaskService taskService){
            _taskService = taskService;
        }
        

        [HttpPost]
        [Route("TaskCreate")]
        
        public bool TaskCreate(TaskCreateDTO task){
            return _taskService.TaskCreate(task);
        }


        [HttpPost]
        [Route("AddComment")]

        public bool AddComment(CommentDTO comment)
        {
            return _taskService.AddComment(comment);
        }

        [HttpPut]
        [Route("EditTask")]

        public bool EditTask(TaskCreateDTO task)
        {
            return _taskService.EditTask(task);
        }


        [HttpGet]
        [Route("GetBoardTask/{boardID}")]

        public IEnumerable<TaskModel> GetBoardTasks(string BoardID)
        {
            return _taskService.GetBoardTasks(BoardID);
        }
        
    }
}