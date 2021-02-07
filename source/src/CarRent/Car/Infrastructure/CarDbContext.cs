using CarRent.Car.Domain;
using CarRent.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Car.Infrastructure
{
    public class CarDbContext : BaseDbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }
        public DbSet<Domain.Car> Car { get; set; }
        public DbSet<CarSpecification> Specification { get; set; }
        public DbSet<CarClass> Class { get; set; }
    }
}
