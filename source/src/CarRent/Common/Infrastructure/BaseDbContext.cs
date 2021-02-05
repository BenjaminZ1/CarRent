using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Domain;
using CarRent.Car.Infrastructure;
using CarRent.Reservation.Infrastructure;
using CarRent.User.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Common.Infrastructure
{

    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }
        protected BaseDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());

            modelBuilder.Entity<CarClass>()
                .Ignore(cls => cls.Cars);

            modelBuilder.Entity<LuxuryCarClass>()
                .HasDiscriminator<string>("class_type")
                .HasValue("class_luxury");

            modelBuilder.Entity<MediumCarClass>()
                .HasDiscriminator<string>("class_type")
                .HasValue("class_medium");

            modelBuilder.Entity<EasyCarClass>()
                .HasDiscriminator<string>("class_type")
                .HasValue("class_easy");

            modelBuilder.Entity<CarClass>()
                .Ignore(c => c.ReservationRef);

            //modelBuilder.Entity<Car.Domain.Car>()
            //    .Ignore(c => c.ClassId)
            //    .HasOne(c => c.Specification)
            //    .WithOne(s => s.Car)
            //    .HasForeignKey<CarSpecification>(s => s.CarRef)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Car.Domain.Car>()
            //    .Ignore(c => c.ClassId)
            //    .HasOne(c => c.Class)
            //    .WithMany(cls => cls.Cars)
            //    .IsRequired(true)
            //    .HasForeignKey(c => c.ClassRef)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<User.Domain.User>()
            //    .HasMany(u => u.Reservations)
            //    .WithOne(r => r.User)
            //    .OnDelete(DeleteBehavior.Cascade);



            //modelBuilder.Entity<Reservation.Domain.Reservation>()
            //    .HasOne(r => r.Class)
            //    .WithOne(c => c.Reservation)
            //    .HasForeignKey<Reservation.Domain.Reservation>(r => r.ClassRef)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Reservation.Domain.Reservation>()
            //    .HasOne(r => r.User)
            //    .WithMany(u => u.Reservations)
            //    .HasForeignKey(r => r.UserRef);


        }
    }

}
