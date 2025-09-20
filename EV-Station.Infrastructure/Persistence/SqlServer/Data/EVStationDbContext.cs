using EV_Station.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EV_Station.Infrastructure.Persistence.SqlServer.Data
{
    public class EVStationDbContext : DbContext
    {
        public EVStationDbContext(DbContextOptions<EVStationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<IdentityCard> IdentityCards { get; set; }
        public DbSet<DriverLicense> DriverLicenses { get; set; }
    }
}
