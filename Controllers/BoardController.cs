using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using task_tracker_group.Models;
using task_tracker_group.Services;

namespace task_tracker_group.Controllers
{
    [ApiController]
    [Route("[controller]")]



    public class BoardController : ControllerBase
    {
        private readonly BoardService _data;

        public BoardController(BoardService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("CreateProject")]
        public bool CreateBoard(BoardModel newBoard)
        {
            return _data.CreateBoard(newBoard);
        }

        [HttpGet]
        [Route("GetAllProjects")]
        public IEnumerable<BoardModel> GetAllProjects()
        {
            return _data.GetAllProjects();
        }

        [HttpGet]
        [Route("GetAllRelations")]
        public IEnumerable<RelationTableModel> GetAllRelations()
        {
            return _data.GetAllRelations();
        }

        [HttpPost]
        [Route("AddUserToProjectByUserId/{userID}/{projectID}/{boardCode}")]
        public bool AddUserToProjectByUserId(int userID, int projectID, string boardCode)
        {
            return _data.AddUserToProjectByUserId(userID, projectID, boardCode);
        }


        [HttpGet]
        [Route("GetProjectByID/{projectID}")]
        public BoardModel GetProjectByID(int projectID)
        {
            return _data.GetProjectByID(projectID);
        }

        [HttpDelete]
        [Route("DeleteProject/{projectID}")]
        public bool DeleteProject(int projectID)
        {
            return _data.DeleteProject(projectID);
        }




    }
}