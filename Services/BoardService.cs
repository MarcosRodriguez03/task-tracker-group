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
            relationModel.BoardID = _context.BoardInfo.Count() + 1;

            _context.Add(relationModel);

            return _context.SaveChanges() != 0;
        }


        public IEnumerable<BoardModel> GetAllProjects()
        {
            return _context.BoardInfo;
        }

        public IEnumerable<RelationTableModel> GetAllRelations()
        {
            return _context.RelationTableInfo;
        }

        public bool AddUserToProjectByUserId(int userID, int projectID, string boardCode)
        {
            RelationTableModel newUser = new RelationTableModel();
            bool result = false;

            if (!IsUserInProject(userID, projectID) && DoesUserExist(userID) && DoesBoardExist(boardCode))
            {

                newUser.UserID = userID;
                newUser.BoardID = projectID;

                _context.Add(newUser);

                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public bool IsUserInProject(int userID, int projectID)
        {
            return _context.RelationTableInfo.SingleOrDefault(check => check.UserID == userID && check.BoardID == projectID) != null;
        }

        public bool DoesUserExist(int userID)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == userID) != null;
        }
        public bool DoesBoardExist(string boardCode)
        {
            return _context.BoardInfo.SingleOrDefault(board => board.BoardCode == boardCode) != null;
        }

        public BoardModel GetProjectByID(int projectID)
        {
            return _context.BoardInfo.SingleOrDefault(project => project.ID == projectID);
        }

        public bool DeleteProject(int projectID)
        {
            BoardModel projectOwner = GetProjectByID(projectID);
            IEnumerable<RelationTableModel> usersWithinProject = GetAllUsersWithinProject(projectID);
            bool result = false;
            if (projectOwner != null)
            {
                projectOwner.IsDeleted = true;
                _context.Update<BoardModel>(projectOwner);
                _context.RemoveRange(usersWithinProject);
                result = _context.SaveChanges() != 0;
            }

            return result;

        }

        public IEnumerable<RelationTableModel> GetAllUsersWithinProject(int projectID)
        {
            return _context.RelationTableInfo.Where(project => project.BoardID == projectID);
        }

    }
}