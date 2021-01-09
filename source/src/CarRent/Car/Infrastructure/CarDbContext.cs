using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Car.Infrastructure
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options){ }
        public DbSet<Domain.Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarSpecification>()
                .HasMany(c => c.Cars)
                .WithOne(e => e.Specification);
        }
    }
}
