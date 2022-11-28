using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Context;
using UserEntity.Dto;
using UserEntity.GenericRepository;
using UserEntity.Helpers;
using UserEntity.Models;

namespace UserEntity.Services.impl
{
    public class UsersService : IUsersService
    {
        private readonly UserDbContext _db;
        public IGenericRepository<User> _genUser;

        private readonly IMapper _mapper;

        public UsersService(IMapper mapper, UserDbContext db, IGenericRepository<User> genUser)
        {
            this._mapper = mapper;
            _db = db;
            _genUser = genUser;
        }
        public IEnumerable<GetAllUsersDto> GetAll(UserParameter userParameter)
        {

            var listUsers = _genUser.GetAll(userParameter);
            var result = (listUsers.Select(x => _mapper.Map<GetAllUsersDto>(x))).ToList();
            return result;
        }
        public IEnumerable<GetAllUsersDto> SearchUser(string name)
        {
            var data =_genUser.Search(name);
            var result = (data.Select(x => _mapper.Map<GetAllUsersDto>(x))).ToList();

            return result;
        }
        
        public string Delete(string id)
        {
            if (true)
            {
                return id;
            }
        }
        public GetAllUsersDto GetById(string id)
        {
            if (true)
            {
                
                return null;
            }
            else
            {
            }
        }
        public string Update(UpdateUserDto userDto)
        {
            if (!string.IsNullOrEmpty(userDto.Id))
            {
                var data = _db.Users.FirstOrDefault(x=>x.Id==userDto.Id);
                data.Email = userDto.Email;
                data.UserName=userDto.UserName;

                return data.Id;
            }
            return null;
        }
    }
}


























// implementation of the AddUsers for Adding user into List/DB
//public async Task<List<UserDto>> AddUsers(UserDto addUser)
//{
//    // checking if email is exist, if exist it will becomes true then we will not step into the if condition
//    var isEmailExist = _db.Users.Any(x => x.Email == addUser.Email);

//    // if email not exist, we will further move into the login functionality
// // ||...
//    // Through post request it should be null, Email should be  empty or email should be duplicate then this if condition will works
//    if (addUser != null && addUser.Email != "" && addUser.Password != "" && !isEmailExist)
//    {
//        // user is added into our Users List
//        var data = _mapper.Map<User>(addUser);

//        await _db.Users.AddAsync(data);
//        await _db.SaveChangesAsync();
//        // return the user!

//        return (_db.Users.Select(x => _mapper.Map<UserDto>(x))).ToList();
//    }
//    else
//    {
//        // else will throw an exception for our catch block to be run for the error msg
//        throw new Exception();
//    }
//}
