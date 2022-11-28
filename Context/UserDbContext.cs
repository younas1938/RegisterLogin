using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Models;

namespace UserEntity.Context
{
 
    public class UserDbContext:IdentityDbContext
    {
        public UserDbContext(DbContextOptions options):base(options){ }

        public DbSet<User> GetUsers { get; set; }

    }
}
