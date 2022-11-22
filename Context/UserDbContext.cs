using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Models;

namespace UserEntity.Context
{
    // an instance of DB Context represents a sesssion with the database,
    // this means we can query the database and save all the changes to our User Entity
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext>options):base(options){ }
        // whenever we want to see a representation of our model in the database,
        // we have to add a DBSet of that model that's how EF know what tables it should create
        public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registrations { get; set; }
    }
}
