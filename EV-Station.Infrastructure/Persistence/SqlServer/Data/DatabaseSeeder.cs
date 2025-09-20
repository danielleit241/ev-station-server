using EV_Station.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EV_Station.Infrastructure.Persistence.SqlServer.Data
{
    public class DatabaseSeeder
    {
        private readonly EVStationDbContext _context;
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly IConfiguration _configuration;

        public DatabaseSeeder(EVStationDbContext context, ILogger<DatabaseSeeder> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SeedData()
        {
            try
            {
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Create database");

                await SeedRolesAsync();
                await SeedProviderAsync();
                await SeedAdminUser();

                _logger.LogInformation("Seed data completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task SeedAdminUser()
        {
            if (await _context.Users.AnyAsync())
            {
                _logger.LogInformation("User Admin already exist, skipping user seeding");
                return;
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name.Equals("Admin"));
            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.Name.Equals("Local"));

            var admin = new User
            {
                Id = Guid.NewGuid(),
                FullName = "ADMIN",
                Email = "admin@gmail.com",
                RoleId = role!.Id,
                ProviderId = provider!.Id,
                PasswordHash = new PasswordHasher<User>().HashPassword(null!, "admin")
            };

            _context.Users.Add(admin);
            await _context.SaveChangesAsync();

        }

        private async Task SeedProviderAsync()
        {
            if (await _context.Providers.AnyAsync())
            {
                _logger.LogInformation("Providers already exist, skipping provider seeding");
                return;
            }

            var providers = new[]
            {
                new Provider{Name = "Local"},
                new Provider{Name = "Google"},
                new Provider{Name = "Facebook"},
                new Provider{Name = "Github"},
                new Provider{Name = "Microsoft"}
            };

            await _context.Providers.AddRangeAsync(providers);
            await _context.SaveChangesAsync();

        }

        private async Task SeedRolesAsync()
        {
            if (await _context.Roles.AnyAsync())
            {
                _logger.LogInformation("Roles already exist, skipping role seeding");
                return;
            }

            var roles = new[]
            {
                new Role {Name = "Renter"},
                new Role {Name = "Staff"},
                new Role {Name = "Admin"}
            };

            await _context.Roles.AddRangeAsync(roles);
            await _context.SaveChangesAsync();

        }
    }
}
