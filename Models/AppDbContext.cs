using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; } // <-- ADD THIS LINE
  
            // You can optionally add OnModelCreating for advanced configurations,
        // but for basic properties, EF Core conventions often suffice.
        // /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Example: modelBuilder.Entity<Teacher>().Property(t => t.Name).IsRequired();
        }
        
    }
}
