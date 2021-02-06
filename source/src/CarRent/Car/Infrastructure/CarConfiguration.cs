using CarRent.Car.Domain;
using CarRent.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Car.Infrastructure
{
    public class CarConfiguration : BaseEntityConfigurations<Domain.Car>
    {
        public override void Configure(EntityTypeBuilder<Domain.Car> builder)
        {
            base.Configure(builder);

            builder
                .Ignore(c => c.ClassId)
                .HasOne(c => c.Specification)
                .WithOne(s => s.Car)
                .HasForeignKey<CarSpecification>(s => s.CarRef)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Ignore(c => c.ClassId)
                .HasOne(c => c.Class)
                .WithMany(cls => cls.Cars)
                .IsRequired(true)
                .HasForeignKey(c => c.ClassRef)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
