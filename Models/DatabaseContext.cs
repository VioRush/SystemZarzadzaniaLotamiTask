using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using SystemZarzadzaniaLotami.Enums;

namespace SystemZarzadzaniaLotami.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var hasher = new PasswordHasher<User>();

            var adminEmail = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SiteSettings")["AdminEmail"];
            var adminPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SiteSettings")["AdminPassword"];

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "80c8b6b1-e2b6-45e8-b044-8f2178a90111", 
                    UserName = "admin",
                    NormalizedUserName = adminEmail.ToUpper(),
                    PasswordHash = hasher.HashPassword(null, adminPassword),
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    Role = Role.Admin
                }
            );
        }
    }
}
