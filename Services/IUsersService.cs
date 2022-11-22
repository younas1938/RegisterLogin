using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Dto;
using UserEntity.Models;

namespace UserEntity.Services
{
    //Interface class With 'I' shows It's Interface
    public interface IUsersService
    {
        // this Interfaces classes are pure abstract and by default it's public and cannot have a body like {}


        // getAll Users, method without body
        Task<List<UserDto>> GetAll();
        // getUser Id=, method without body
        Task<UserDto> GetById(int id);
        // Register/Add user, without body
        Task<List<UserDto>> AddUsers(UserDto user);
        // update user method without body
        Task<UserDto> Update(UserDto newUser);
        // Delete user mehthod without body
        Task<List<UserDto>> Delete(int id);

    }
}
