using Microsoft.AspNetCore.Mvc;
using UserEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Services;
using UserEntity.Helpers;
using UserEntity.Dto;

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
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = _user.GetAll();
                // it will call the override OK method which is in the BaseController
                // OK will return the 200 successful http request
                return Ok( await users);
            }
            catch (Exception)
            {
                // this badrequest generatre a Server Error message 'Internal Server Error'
                return BadRequest(HelperMessage.serverError) ;
            }
        }
        // HttpGet("{id}") this will route into the specific id request likes Users/2
        [HttpGet("{id}")]
        public async Task<IActionResult> UserDetails(int id)
        {
            try
            {
                // will get user obj by it's id with in Try block incase of error
                var user = _user.GetById(id);
                // Ok returns to JSON form with some generic response (status:1, message:"")
                return Ok(await user);
            }
            catch (Exception)
            {
                // incase of user wrong entry or any other issues badrequest will returen a generic msg Server Error
                return BadRequest(Helpers.HelperMessage.serverError);
            }
        }
        

        // using httpDelete request to remove user from list/db by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var data = await _user.Delete(id);
                return Ok(  HelperMessage.userDeleted);

            }
            catch (Exception)
            {

                return BadRequest(HelperMessage.serverError);
            }
        }
        // post is used to add data into List/DB
        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto addUser)
        {
            try
            {
                // using UserService to add user in the Try block if no error occurs
                var data =await _user.AddUsers(addUser);
                // if no error occurs try block will run the OK request to generate a msg 'User Added!'
                return Ok(HelperMessage.userAdded);
            }
            catch (Exception)
            {
                // incase of any error occurs, the catch block runs a badrequest, which will generate a 'Internal Server Error' msg
                return BadRequest(HelperMessage.serverError);
            }
        }
        // post is used to add data into List/DB
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto updateUser)
        {
            try
            {
                // using UserService to add user in the Try block if no error occurs
                var data =await _user.Update(updateUser);
                // if no error occurs try block will run the OK request to generate a msg 'User Added!'
                return Ok(HelperMessage.userUpdated);
            }
            catch (Exception)
            {
                // incase of any error occurs, the catch block runs a badrequest, which will generate a 'Internal Server Error' msg
                return BadRequest(HelperMessage.serverError);
            }
        }
    }
}
