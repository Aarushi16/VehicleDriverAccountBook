using AuthorizationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationAPI.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
