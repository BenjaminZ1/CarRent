using CarRent.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarRent.User.Infrastructure
{
    public class UserDbContext : BaseDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<Domain.User> User { get; set; }
    }
}
