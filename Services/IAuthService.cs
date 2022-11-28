using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Dto;
using UserEntity.Models;

namespace UserEntity.Services
{
    public interface IAuthService 
    {

        Task<string> Register(UserRegistrationDto user);

        Task<string> Login(LoginUserDto user);
        Task<bool> EmailExists(string email);
        Task<bool> ConfirmEmailAsync(string userId, string token);
    }
}
