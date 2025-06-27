using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Model;

namespace ConsoleApp1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Alice", Email = "alice@example.com", Username = "alice123" },
                new User { Id = 2, Name = "Bob", Email = "bob@example.com", Username = "bob123" }
            );
        }
    }
}