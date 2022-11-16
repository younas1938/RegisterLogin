using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            return Ok(user);
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var singleUser = user.FirstOrDefault(x => x.Id == id);
            if (singleUser!=null && id>0 )
            {
                return Ok(singleUser);
            }
            else
            {
                return BadRequest();
            }
            
        }
        [HttpPut]
        public IActionResult UpdateUser(User updateUser)
        {
            // list/array is reference type so character will automaticlly update the origanal list
            var userObj = user.FirstOrDefault(u => u.Id == updateUser.Id);
            if (userObj != null && updateUser.Email is null)
            {
                userObj.FirstName = (updateUser.FirstName);
                userObj.LastName = updateUser.LastName;
                userObj.UserName = updateUser.UserName;
                userObj.Password = updateUser.Password;
                return Ok("Updated!");
            }
            else
            {
                return BadRequest();
            }

            
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (user.Any(x => x.Id == id))
            {
                var userDel = user.Find(x => x.Id == id);
                user.Remove(userDel);
                return Ok("User Deleted");
            }
            else
            {
                return BadRequest("Invalid");
            }
           
        }
        
        [HttpPost]
        public IActionResult AddUser(User addUser)
        {
            var emailVerfication = user.Any(x => x.Email == addUser.Email);
            
            if (addUser!=null && addUser.Email!= "" && addUser.Password != null && !emailVerfication)
            {
                addUser.Id = user.Count+1;
                user.Add(addUser);
                return Ok("User Added");
            }
            else
            {
                return BadRequest("Invalid");
            }
           
        }

        [HttpPost("login")]
        public IActionResult LoginUser(User loginUser)
        {
            var userss = user.FirstOrDefault(x => x.UserName == loginUser.UserName && x.Password == loginUser.Password);
            if (userss!=null)
            {
                return Ok("You are Logged In");
            }
            else
            {
               
                return BadRequest("You have entered Wrong Username | Password");
            }
        }
    }
}
