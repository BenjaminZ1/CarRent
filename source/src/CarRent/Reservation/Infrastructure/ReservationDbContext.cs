using CarRent.Car.Domain;
using CarRent.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Reservation.Infrastructure
{
    public class ReservationDbContext : BaseDbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options) {}

        public DbSet<Domain.Reservation> Reservation { get; set; }
        public DbSet<Car.Domain.CarClass> Class { get; set; }
        public DbSet<User.Domain.User> User { get; set; }




        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Ignore<Car.Domain.CarClass>();

        //    modelBuilder.Entity<Domain.Reservation>()
        //        .Ignore(r => r.ClassRef)
        //        .HasOne(r => r.Class)
        //        .WithOne(c => c.Reservation)
        //        .HasForeignKey<CarClass>(c => c.ReservationRef)
        //        .OnDelete(DeleteBehavior.SetNull);

        //}
    }
}
