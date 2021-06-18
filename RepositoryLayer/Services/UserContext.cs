using CommonLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
   public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<Users> Users { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasData(new User
        //    {
        //        UserId = 1,
        //        FirstName = "Uncle",
        //        LastName = "Bob",
        //        Email = "uncle.bob@gmail.com",                
        //        Password = "999-888-7777"
        //    }, new User
        //    {
        //        UserId = 2,
        //        FirstName = "Jan",
        //        LastName = "Kirsten",
        //        Email = "jan.kirsten@gmail.com",               
        //        Password = "111-222-3333"
        //    });
        //}
    }


}
