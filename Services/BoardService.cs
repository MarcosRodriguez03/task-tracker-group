using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_tracker_group.Services
{
    public class BoardService
    {
        public bool CreateBoard(BoardModel newBoard)
        {
            newBoard.IsDeleted = false;
            _context.Add(newBoard);
            
            RelationTableModel relationModel = new RelationTableModel();
            
            relationModel.UserID = newBoard.UserID;
            relationModel.ProjectID = _context.ProjectInfo.Count() + 1;

            _context.Add(relationModel);

            return _context.SaveChanges() != 0;
        }
    }
}