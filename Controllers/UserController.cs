using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Controllers
{
    // used to the all types and derived types serve as HTTP respnses
    [ApiController]
    // ApiController enables several featues like attribute Routing
    // specific controller call when web services usings
    [Route("[controller]")]
    public class UserController : BaseController
    {
        // httpGet will return, in our case we are returning the Users List
        [HttpGet("Users")]
        // GetUsers will return the All Users in POSTMAN Get Request
        public IActionResult GetUsers()
        {
            // OK is will return the 200 successful http request
            return Ok(user);
        }
        // HttpGet("{id}") this will route into the specific id request likes Users/2
        [HttpGet("{id}")]
        // GetUserById will return a specific parameter/id values
        public IActionResult GetUserById(int id)
        {
            // FirstOrDefault used to get the first id which they found in our list/DB?
            // if found, then will return a complete object/Row
            var returnUserById = user.FirstOrDefault(x => x.Id == id);
            
            // we are checking our object => not null(!null) and should be greater thn 0...
            if (returnUserById != null && id>0 )
            {
                // will return the specific user details
                return Ok(returnUserById);
            }
            else
            {
                // return the badrequest incase of wrong data type or syntax error
                return BadRequest("Invalid");
            }
            
        }
        // [HttpPut] is used for the Update of the data/user
        [HttpPut]
        // we will be parsing data/json user details, which particular user details we want to update by using his id
        public IActionResult UpdateUser(User updateUser)
        {
            // list/array is reference type so userObj will automaticlly update the origanal list 'user'
            var userObj = user.FirstOrDefault(u => u.Id == updateUser.Id);

            // this is the most important part by checking email is updating or not if updating we will return badrequest
            // ...in the business module, once email is created it can not be changed 
            if (userObj != null && updateUser.Email is null)
            {
                // updating the usersList by postman Put Request
                userObj.FirstName = (updateUser.FirstName);
                userObj.LastName = updateUser.LastName;
                userObj.UserName = updateUser.UserName;
                userObj.Password = updateUser.Password;
                return Ok("Updated!");
            }
            else
            {
                return BadRequest("Invalid");
            }

        }
        // using the POSTMAN DELETE Request by providing the id
        [HttpDelete("{id}")]
        // DeleteUser will get the specific id which is provided by the POSTMAN DEL request
        public IActionResult DeleteUser(int id)
        {
            // this will check if it's true will go into If
            var isUserAvailable = user.Any(x => x.Id == id);
            if (isUserAvailable)
            {
                // it will find the specific id and return the complete row/obj
                var userDel = user.Find(x => x.Id == id);
                // remove the obj from the list
                user.Remove(userDel);
                // response deleted
                return Ok("User Deleted");
            }
            else
            {
                // reponse is if user added wrong id
                return BadRequest("Invalid");
            }
           
        }
        // post request will use to Add user/details in our application Model
        [HttpPost]
        // through Post Request we will add a user into AddUser Action method
        public IActionResult AddUser(User addUser)
        {
            // email verification, either email exists or not, if exists it will become true otherwise false
            var emailVerfication = user.Any(x => x.Email == addUser.Email);
            // if email not exist, we will further move into the login functionality
            
            // Through post request it should be null, Email should be  empty or email should be duplicate then this if condition will works
            if (addUser!=null && addUser.Email!= "" && addUser.Password != null && !emailVerfication)
            {
                // userId will be the usersList last index but with one Increment for saving into the next empty list of index
                addUser.Id = user.Count+1;
                // user is added into our Users List
                user.Add(addUser);
                // return the response of User Added!
                return Ok("User Added!");
            }
            else
            {
                // otherwise response will be invalid
                return BadRequest("Invalid");
            }
           
        }
        
        [HttpPost("login")]
        // login user will get only two field, they're Email and password
        public IActionResult LoginUser(User loginUser)
        {
            // this will return object/row if and only if both Email and Password is true  
            var userss = user.FirstOrDefault(x => x.Email == loginUser.Email && x.Password == loginUser.Password);
            if (userss!=null)
            {
                // login successful response
                return Ok("Login successful!");
            }
            else
            {
               // bad request or invalid if any of them is false
                return BadRequest("Invalid");
            }
        }
    }
}
