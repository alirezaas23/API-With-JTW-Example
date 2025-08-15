using JWTTraining.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTTraining.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {            
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
