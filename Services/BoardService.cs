using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using task_tracker_group.Models;
using task_tracker_group.Services.Context;

namespace task_tracker_group.Services
{
    public class BoardService : ControllerBase
    {
     private readonly DataContext _context;
        public BoardService(DataContext context)
        {
            _context = context;
        }

        public bool SaveChangesToDataBase()
        {
            return _context.SaveChanges() != 0;
        }

        public bool CreateBoard(BoardModel newBoard)
        {

            const string stringOfChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            Random random = new Random();

             string Code = "";
             for (int i = 0; i < 6; i++)
            {
                int randomNumber = random.Next(1, stringOfChars.Length + 1);
               
               Code += stringOfChars[randomNumber];
            }
            
            newBoard.IsDeleted = false;
            newBoard.BoardCode = Code;

            _context.Add(newBoard);
            
            RelationTableModel relationModel = new RelationTableModel();
            
            relationModel.UserID = newBoard.UserID;
            relationModel.ProjectID = _context.BoardInfo.Count() + 1;

            _context.Add(relationModel);

            return _context.SaveChanges() != 0;
        }


        //  public IEnumerable<BoardModel> GetAllProjects()
        // {
        //     return _context.BoardInfo;
        // }


    }
}