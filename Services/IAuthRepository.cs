using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Models;

namespace UserEntity.Services
{
    public interface IAuthRepository 
    {

        Task<int> Register(Registration user, string password);

        Task<string> Login(string email, string password);
        Task<bool> UserExists(string username);
    }
}
