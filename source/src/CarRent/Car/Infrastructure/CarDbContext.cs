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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<User.Domain.User>();

            modelBuilder.Entity<LuxuryCarClass>()
                .HasDiscriminator<string>("class_type")
                .HasValue("class_luxury");

            modelBuilder.Entity<MediumCarClass>()
                .HasDiscriminator<string>("class_type")
                .HasValue("class_medium");

            modelBuilder.Entity<EasyCarClass>()
                .HasDiscriminator<string>("class_type")
                .HasValue("class_easy");

            modelBuilder.Entity<Domain.Car>()
                .Ignore(c => c.ClassId)
                .HasOne(c => c.Specification)
                .WithOne(s => s.Car)
                .HasForeignKey<CarSpecification>(s => s.CarRef)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Car>()
                .Ignore(c => c.ClassId)
                .HasOne(c => c.Class)
                .WithMany(cls => cls.Cars)
                .IsRequired(true)
                .HasForeignKey(c => c.ClassRef)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
