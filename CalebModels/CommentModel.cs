using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_tracker_group.CalebModels.DTO
{
    public class CommentModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public DateTime DateMade { get; set; }
        public int UserID { get; set; }
    }

    public CommentModel()
    {
        DateMade = DateTime.Now;
    }
}