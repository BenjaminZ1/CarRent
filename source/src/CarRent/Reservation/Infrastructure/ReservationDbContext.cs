using CarRent.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Reservation.Infrastructure
{
    public class ReservationDbContext : BaseDbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options) { }

        public DbSet<Domain.Reservation> Reservation { get; set; }
        public DbSet<Car.Domain.CarClass> Class { get; set; }
        public DbSet<User.Domain.User> User { get; set; }
    }
}
