using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_tracker_group.Models
{
    public class RelationTableModel
    {
    public int ID { get; set; }

    public int ProjectID { get; set; }

  
    public int UserID { get; set; }
    public RelationTableModel()
    {
        
    }
    }
}