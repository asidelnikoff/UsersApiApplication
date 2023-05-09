using Microsoft.EntityFrameworkCore;
using UsersApiApplication.Models;

namespace UsersApiApplication.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserGroup> UsersGroup { get; set; } = null!;
        public DbSet<UserState> UsersState { get; set; } = null!;

        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>().Property(e => e.Code).HasConversion<string>();
            modelBuilder.Entity<UserState>().Property(e => e.Code).HasConversion<string>();
        }
    }
}
