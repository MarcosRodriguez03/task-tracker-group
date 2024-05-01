using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_tracker_group.Models
{
    public class BoardModel
    {
      
        // project ID
        public int ID { get; set; }
        
        // user who created project
        public int UserID { get; set; }

        public string? ProjectName { get; set; }

        public string BoardCode {get; set; }

        public bool IsDeleted { get; set; }

        public BoardModel()
        {

        }
    }
}