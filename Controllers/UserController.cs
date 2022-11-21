using Microsoft.AspNetCore.Mvc;
using UserEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Services;
using UserEntity.Helpers;

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
            try
            {
                var users = _user.GetAll();
                // it will call the override OK method which is in the BaseController
                // OK will return the 200 successful http request
                return Ok(users);
            }
            catch (Exception)
            {
                // this badrequest generatre a Server Error message 'Internal Server Error'
                return BadRequest(Helpers.HelperMessage.serverError) ;
            }
        }
        // HttpGet("{id}") this will route into the specific id request likes Users/2
        [HttpGet("{id}")]
        public IActionResult UserDetails(int id)
        {
            try
            {
                // will get user obj by it's id with in Try block incase of error
                var user = _user.GetById(id);
                // Ok returns to JSON form with some generic response (status:1, message:"")
                return Ok(user);
            }
            catch (Exception)
            {
                // incase of user wrong entry or any other issues badrequest will returen a generic msg Server Error
                return BadRequest(Helpers.HelperMessage.serverError);
            }
        }
        // Put is use for the Update in our List/DB using API
        [HttpPut]
        public IActionResult UpdateUser(User updateUser)
        {
            try
            {
                // using try block, to update user by using the update service
                var data = _user.Update(updateUser);
                // Ok request will return a nice msg 'User Updated' by using custome serverResponse
                return Ok(Helpers.HelperMessage.userUpdated);
            }
            catch (Exception)
            {
                // incase of exception, the badrequest will generate a custome server response error message 'Internal Server Error'
                return BadRequest(Helpers.HelperMessage.serverError);
            }
        }
        // using httpDelete request to remove user from list/db by id
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                // using try block to check if any error occurs for the remove the user by using delete userservice
                _user.Delete(id);
                // if user deleted with showing no error, OK request will generate a msg "User Deleted!"
                return Ok(Helpers.HelperMessage.userDeleted);
            }
            catch (Exception)
            {
                // incase of exception arise, the badrequest will generate a 'Internal Server error' msg 
                return BadRequest(Helpers.HelperMessage.serverError);
            }
        }
        // post is used to add data into List/DB
        [HttpPost]
        public IActionResult AddUser(User addUser)
        {
            try
            {
                // using UserService to add user in the Try block if no error occurs
                var data = _user.AddUsers(addUser);
                // if no error occurs try block will run the OK request to generate a msg 'User Added!'
                return Ok(Helpers.HelperMessage.userAdded);
            }
            catch (Exception)
            {
                // incase of any error occurs, the catch block runs a badrequest, which will generate a 'Internal Server Error' msg
                return BadRequest(Helpers.HelperMessage.serverError);
            }
        }
    }
}
