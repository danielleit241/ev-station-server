using EV_Station.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EV_Station.Infrastructure.Persistence.Data
{
    public class EVStationDbContext : DbContext
    {
        public EVStationDbContext(DbContextOptions<EVStationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}
