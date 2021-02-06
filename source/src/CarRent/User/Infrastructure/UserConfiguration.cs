using CarRent.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.User.Infrastructure
{
    public class UserConfiguration : BaseEntityConfigurations<Domain.User>
    {
        public override void Configure(EntityTypeBuilder<Domain.User> builder)
        {
            base.Configure(builder);

            builder
            .HasMany(u => u.Reservations)
            .WithOne(r => r.User)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
