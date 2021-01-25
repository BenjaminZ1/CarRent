using CarRent.Car.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarRent.User.Infrastructure
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<CarDbContext> options) : base(options) { }

        public DbSet<Domain.User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.User>();
        }
    }
}
