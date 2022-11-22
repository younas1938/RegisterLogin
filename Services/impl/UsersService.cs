using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Context;
using UserEntity.Dto;
using UserEntity.Models;

namespace UserEntity.Services.impl
{
    // Service class for the User Entity which is implemented from the Interface
    public class UsersService : IUsersService
    {

        private readonly IMapper _mapper;
        private readonly UserDbContext _db;
        public UsersService(IMapper mapper, UserDbContext db)
        {
            this._mapper = mapper;
            _db = db;
        }
        // implementation of the AddUsers for Adding user into List/DB
        public async Task<List<UserDto>> AddUsers(UserDto addUser)
        {
            // checking if email is exist, if exist it will becomes true then we will not step into the if condition
            var isEmailExist = _db.Users.Any(x => x.Email == addUser.Email);

            // if email not exist, we will further move into the login functionality
         // ||...
            // Through post request it should be null, Email should be  empty or email should be duplicate then this if condition will works
            if (addUser != null && addUser.Email != "" && addUser.Password != "" && !isEmailExist)
            {
                // user is added into our Users List
                var data = _mapper.Map<User>(addUser);
                await _db.Users.AddAsync(data);
                await _db.SaveChangesAsync();
                // return the user!
         
                return (_db.Users.Select(x => _mapper.Map<UserDto>(x))).ToList();
            }
            else
            {
                // else will throw an exception for our catch block to be run for the error msg
                throw new Exception();
            }
        }
        // DeleteUser will get the specific id which is provided by the POSTMAN DEL request
        public async Task<List<UserDto>> Delete(int id)
        {
            try
            {
                var dbDelUser = await _db.Users.FirstAsync(x => x.Id == id);
                _db.Users.Remove(dbDelUser);
                await _db.SaveChangesAsync();

                var data = (_db.Users.Select(x => _mapper.Map<UserDto>(x))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // get list of users
        public async Task<List<UserDto>> GetAll()
        {
            // we will get list of users from our inMemory list => Users
            IEnumerable<User> dbUsers = await _db.Users.ToListAsync();
            var result = (dbUsers.Select(x => _mapper.Map<UserDto>(x))).ToList();
            return result;
        }
        // get a specific user details by his/her id
        public async Task<UserDto> GetById(int id)
        {
            // will return the first found obj otherwise null 
            var dbUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            var result = _mapper.Map<UserDto>(dbUser);
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
        public async Task<UserDto> Update(UserDto updateUser)
        {

            try
            {
                var dbUpdate = await _db.Users.FirstOrDefaultAsync(x => x.Id == updateUser.Id && string.IsNullOrEmpty(updateUser.Email));
                dbUpdate.UserName = updateUser.UserName;
                //dbUpdate.Password = updateUser.Password;
                _db.Users.Update(dbUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<UserDto>(dbUpdate);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
