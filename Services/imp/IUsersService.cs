using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Models;

namespace UserEntity.Data.Services
{
    //Interface class With 'I' shows It's Interface
    public interface IUsersService
    {
        // this Interfaces classes are pure abstract and by default it's public and cannot have a body like {}


        // getAll Users, method without body
        List<User> GetAll();
        // getUser Id=, method without body
        User GetById(int id);
        // Register/Add user, without body
        List<User> AddUsers(User user);
        // update user method without body
        User Update(User newUser);
        // Delete user mehthod without body
        void Delete(int id);

                                    /* Interface doesnt have a body { }*/
    }
}
