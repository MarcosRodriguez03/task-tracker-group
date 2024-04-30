using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_tracker_group.Models.DTO
{
    public class CommentDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public DateTime DateMade { get; set; }
        public int UserID { get; set; }
    }
}