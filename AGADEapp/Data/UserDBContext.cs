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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Login = "test1@fake.fake", Password = "Password1", UserData = new UserData() },
                new User() { Id = 2, Login = "test2@fake.fake", Password = "Password2", UserData = new UserData() },
                new User() { Id = 3, Login = "admin1@fake.fake", Password = "AdminPassword1", UserData = new UserData() }
            );

            modelBuilder.Entity<UserData>().HasData(
                new UserData() { Id = 1, IsAdmin = false, Name = "Jan", Surname = "Kowalski", Nickname = "JKowak", UserId = 1 },
                new UserData() { Id = 2, IsAdmin = false, Name = "Magda", Surname = "Łokietek", Nickname = "Madziaaaa", UserId = 2 },
                new UserData() { Id = 3, IsAdmin = true, Name = null, Surname = null, Nickname = "", UserId = 3 }
           );
        }
    }
}