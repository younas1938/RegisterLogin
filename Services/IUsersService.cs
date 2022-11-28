using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Dto;
using UserEntity.Models;

namespace UserEntity.Services
{

    public interface IUsersService
    {
        IEnumerable<GetAllUsersDto> SearchUser(string name);
        IEnumerable<GetAllUsersDto> GetAll(UserParameter userParameter);
        GetAllUsersDto GetById(string id);

        string Update(UpdateUserDto updateuser);
        string Delete(string id);

    }
}
