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

            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        }
    }

}
