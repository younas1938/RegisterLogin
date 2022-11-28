using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Dto;
using UserEntity.Models;

namespace UserEntity
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IdentityUser, GetAllUsersDto>();
            CreateMap<UserRegistrationDto, IdentityUser>();

        }

    }
}
