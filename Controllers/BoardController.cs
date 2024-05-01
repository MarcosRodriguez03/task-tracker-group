using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            return _data.CreateProject(newBoard);
        }
        


        
    }
}