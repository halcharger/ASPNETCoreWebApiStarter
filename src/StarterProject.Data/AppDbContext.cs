using System;
using Microsoft.EntityFrameworkCore;
using StarterProject.Data.Entities;

namespace StarterProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public void SeedData()
        {
            var users = new[]
{
                new User {Id = 1, Email = $"{Guid.NewGuid()}@gmail.com", FullName = Guid.NewGuid().ToString()},
                new User {Id = 2, Email = $"{Guid.NewGuid()}@gmail.com", FullName = Guid.NewGuid().ToString()},
                new User {Id = 3, Email = $"{Guid.NewGuid()}@gmail.com", FullName = Guid.NewGuid().ToString()},
            };

            Users.AddRange(users);
            SaveChanges();
        }
    }
}