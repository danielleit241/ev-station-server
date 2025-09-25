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
        public DbSet<AdditionalCharge> AdditionalCharges { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalImage> RentalImages { get; set; }
        public DbSet<RentalLocation> RentalLocations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleImage> VehicleImages { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleHistory> VehicleHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.PickupLocation)
                .WithMany(l => l.Pickups)
                .HasForeignKey(r => r.PickupLocationID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.ReturnLocation)
                .WithMany(l => l.Returns)
                .HasForeignKey(r => r.ReturnLocationID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
