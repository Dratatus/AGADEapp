using AGADEapp.Models;
using Microsoft.EntityFrameworkCore;

namespace AGADEapp.Data.Configration
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<UserData> Data { get; set; }
    }
}