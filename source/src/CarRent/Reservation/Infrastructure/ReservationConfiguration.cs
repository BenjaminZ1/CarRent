using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Reservation.Infrastructure
{
    public class ReservationConfiguration : BaseEntityConfigurations<Domain.Reservation>
    {
        public override void Configure(EntityTypeBuilder<Domain.Reservation> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(r => r.Class)
                .WithOne(c => c.Reservation)
                .HasForeignKey<Reservation.Domain.Reservation>(r => r.ClassRef)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserRef);
        }
    }
}
