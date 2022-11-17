using Microsoft.AspNetCore.Mvc;
using UserEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Data.Services;
using UserEntity.HelperMessage;

namespace UserEntity.Controllers
{
    // used to the all types and derived types serve as HTTP respnses
    [ApiController]
    // ApiController enables several featues like attribute Routing
    // specific controller call when web services usings
    [Route("[controller]")]

    public class UserController : BaseController
    {
        
        private readonly IUsersService _user;
        // added our Service for the User
        public UserController(IUsersService user)
        {
            _user = user;
        }

        // httpGet will return, in our case we are returning the Users from the  List/db
        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            var data = _user.GetAll();
            // it will call the override OK method which is in the BaseController
            // OK will return the 200 successful http request
            return Ok(data);
        }
        // HttpGet("{id}") this will route into the specific id request likes Users/2
        [HttpGet("{id}")]
        public IActionResult UserDetails(int id)
        {

            try
            {
                var dataById = _user.GetById(id);
                return Ok(dataById);
            }
            catch (Exception)
            {

                return BadRequest(HelperMessage.HelperMessage.exceptionError);
            }

        }
        [HttpPut]
        public IActionResult UpdateUser(User updateUser)
        {
            try
            {
                var data = _user.Update(updateUser);
                return Ok(HelperMessage.HelperMessage.userUpdated);
            }
            catch (Exception)
            {

                return BadRequest(HelperMessage.HelperMessage.exceptionError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _user.Delete(id);
                return Ok(HelperMessage.HelperMessage.userDeleted);
            }
            catch (Exception)
            {

                return BadRequest(HelperMessage.HelperMessage.exceptionError);
            }






        }

        [HttpPost]
        public IActionResult AddUser(User addUser)
        {

            try
            {
                var data = _user.AddUsers(addUser);
                return Ok(HelperMessage.HelperMessage.userAdded);
            }
            catch (Exception)
            {

                return BadRequest();
            }



        }

        //[HttpPost("login")]
        //public IActionResult LoginUser(User loginUser)
        //{
        //    var userss = user.FirstOrDefault(x => x.Email == loginUser.Email && x.Password == loginUser.Password);
        //    if (userss!=null)
        //    {
        //        return Ok("Login successful!");
        //    }
        //    else
        //    {
        //        return BadRequest("Invalid");
        //    }
        //}
    }
}
