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
    public class RegisterController : Controller
    {
        //private static Register user = new Register();

        private static List<Register> user = new List<Register>()
        {   new Register(),
            new Register {
                Id=2,
                FirstName="qwer",
                LastName="eq",
                UserName="qwer12",
                Email="qwer12@gmail.com",
                Password="qwer1212"
            },
             new Register {
                Id=3,
                FirstName="zxc",
                LastName="xz",
                UserName="zxc12",
                Email="zxc12@gmail.com",
                Password="zxc1212"
            },
               new Register {
                Id=4,
                FirstName="dfgd",
                LastName="dfg",
                UserName="fgg",
                Email="fgg12@gmail.com",
                Password="fgg1212"
            }
        };
        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            return Ok(user);
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(user.FirstOrDefault(x => x.Id == id));
        }
        [HttpPut]
        public IActionResult UpdateUser(Register updateUser)
        {
            // list/array is reference type so character will automaticlly update the origanal list
            var updtUser = user.FirstOrDefault(u => u.Id == updateUser.Id);
            updtUser.FirstName = updateUser.FirstName;
            updtUser.LastName = updateUser.LastName;
            updtUser.Password = updateUser.Password;
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var userDel = user.Find(x => x.Id == id);
            user.Remove(userDel);
            return Ok(user);
        }
        [HttpPost]
        public IActionResult RegisterUser(Register addUser)
        {
            if (addUser!=null && addUser.Email!= "" && addUser.Password != null)
            {
                user.Add(addUser);
                return Ok("Your Account is Registerd");

            }
            else
            {
                return BadRequest("You have Not Entered the Email | Password");
            }
           
        }
        [HttpPost("login")]
        public IActionResult LoginUser(Register loginUser)
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
