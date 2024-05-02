using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using task_tracker_group.Models;
using task_tracker_group.Models.DTO;
using task_tracker_group.Services;

namespace task_tracker_group.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _data;

        public UserController(UserService data)
        {
            _data = data;
        }


        //Login Endpoint
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO User)
        {
            return _data.Login(User);
        }

        [HttpPost]
        [Route("AddUser")]

        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            return _data.AddUser(UserToAdd);
        }

        [HttpPut]
        [Route("UpdateUser")]

        public bool UpdateUser(UserModel userToUpdate)
        {
            return _data.UpdateUser(userToUpdate);
        }
        [HttpPut]
        [Route("UpdateUser/{id}/{username}")]
        public bool updateUser(int id, string username)
        {
            return _data.UpdateUsername(id, username);
        }

        [HttpDelete]
        [Route("DeleteUser/{userToDelete}")]
        public bool DeleteUser(string userToDelete)
        {
            return _data.DeleteUser(userToDelete);
        }

        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public UserIdDTO GetUserByUsername(string username)
        {
            return _data.GetUserIdDTOByUsername(username);
        }

        [HttpPut]
        [Route("UpdateUserInfo")]
        public IActionResult UpdateUserInfo(UserModel updateUser)
        {

            return _data.UpdateUserInfo(updateUser);
        }

        [HttpGet]

        [Route("GetProfileByUserID/{id}")]

        public UserModel GetProfileByUserID(int id)
        {
            return _data.GetProfileByUserID(id);
        }

        [HttpPut]
        [Route("UpdateUserColor/{userId}/{colorId}")]
        public bool UpdateUserColor(int userId, int colorId)
        {
            return _data.UpdateUserColor(userId, colorId);
        }

        [HttpPut]
        [Route("UpdateUserImage")]
        public IActionResult UpdateUserImage(UserModel userObject)
        {
            return _data.UpdateUserImage(userObject);
        }



    }
}