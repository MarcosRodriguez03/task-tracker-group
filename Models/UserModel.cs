using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_tracker_group.Models
{
    public class UserModel
    {
        public int ID { get; set; }

        public string? Username { get; set; }

        public string? Image { get; set; }
        
        public int? ColorId { get; set; }

        public DateTime? DateMade { get; set; }

        public string? Salt { get; set; }
        
        public string? Hash { get; set; }

        public UserModel()
        {
                  
        }
    }
}