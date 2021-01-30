using CarRent.Car.Domain;
using CarRent.Car.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarRent.User.Infrastructure
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<Domain.User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<Car.Domain.Car>();
            modelBuilder.Ignore<CarSpecification>();
            modelBuilder.Ignore<CarClass>();
            modelBuilder.Entity<Domain.User>();
        }
    }
}
