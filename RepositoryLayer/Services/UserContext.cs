using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
   public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
    }
}
