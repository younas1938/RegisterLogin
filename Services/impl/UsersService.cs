using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Models;

namespace UserEntity.Services.impl
{
    // Service class for the User Entity which is implemented from the Interface
    public class UsersService : IUsersService
    {
        // mock list for the WEBAPI CRUD Implementation
        public static List<User> user = new List<User>()
        {   new User {Id=1,FirstName="qwer",LastName="eq",UserName="qwer12",Email="qwer12@gmail.com",Password="qwer1212"},
         new User {Id=2,FirstName="qwer",LastName="eq",UserName="qwer12",Email="qwer12@gmail.com",Password="qwer1212"}

        };
        // implementation of the AddUsers for Adding user into List/DB
        public List<User> AddUsers(User addUser)
        {
            // checking if email is exist, if exist it will becomes true then we will not step into the if condition
            var isEmailExist = user.Any(x => x.Email == addUser.Email);

            // if email not exist, we will further move into the login functionality
         // ||...
            // Through post request it should be null, Email should be  empty or email should be duplicate then this if condition will works
            if (addUser != null && addUser.Email != "" && addUser.Password != "" && !isEmailExist)
            {
                // userId will be the usersList last index but with one Increment for saving into the next empty list of index
                addUser.Id = user.Count + 1;
                // user is added into our Users List
                user.Add(addUser);
                // return the user!
                return user;
            }
            else
            {
                // else will throw an exception for our catch block to be run for the error msg
                throw new Exception();
            }
        }
        // DeleteUser will get the specific id which is provided by the POSTMAN DEL request
        public void Delete(int id)
        {
            // it will find the specific id and return the complete row/obj or It will be Null
            var isUserExist = user.FirstOrDefault(x => x.Id == id);
            // if isUserExist have no obj it will becomes null and if-condition will throw an exception
            if (isUserExist == null)
            {
                // else will throw an exception for our catch block to be run for the error msg
                throw new Exception();
            }
            // if not null the else condition will remove the user from the list/db
            else
            {
                // will remove the user from the list/db
                user.Remove(isUserExist);
            }
        }
        // get list of users
        public List<User> GetAll()
        {
            // we will get list of users from our inMemory list => Users
            var result = user.ToList();
            return result;
        }
        // get a specific user details by his/her id
        public User GetById(int id)
        {
            // will return the first found obj otherwise null 
            var result = user.FirstOrDefault(x => x.Id == id);
            // if not null the details of a particular user will return
            if (result != null)
            {
                // return the specific user detailss in the UserController
                return result;
            }
            else
            {
                // this exception will run to throw an exception for our Catch block to generate a Badrequest generic Error Msg
                throw new Exception();
            }
        }
        // update the user by sending the PUT from the PSOTMAN
        public User Update(User newUser)
        {
            // getting the user data for updating if id found and email should be null
            var userObj = user.FirstOrDefault(u => u.Id == newUser.Id && newUser.Email is null);
            // if userObj is null due to no userName found , will throw an exception inthe IF condition
            if (userObj == null)
            {
                // this will throw an exception for our Catch block to generate a generic response error msg
                throw new Exception();
            }
            else
            {
                // if userFound for update it will update the user
                userObj.UserName = newUser.UserName;
                userObj.Password = newUser.Password;
                // will return UserObj into the Try Block of the UpdateUser IAction
                return userObj;
            }
        }
    }
}
