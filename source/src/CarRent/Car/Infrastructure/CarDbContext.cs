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
        public DbSet<Domain.Car> Car { get; set; }
        public DbSet<CarSpecification> Specification { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<CarSpecification>()
            //    .HasMany(c => c.Cars)
            //    .WithOne(e => e.Specification)
            //    .HasForeignKey(e => e.Id)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Domain.Car>()
                .HasOne(c => c.Specification)
                .WithMany(e => e.Cars)
                .HasForeignKey(e => e.CarSpecificationId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
