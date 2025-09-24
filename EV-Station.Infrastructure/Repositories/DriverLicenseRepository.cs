using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Domain.Models;
using EV_Station.Infrastructure.Persistence.SqlServer.Data;
using EV_Station.Infrastructure.Repositories.BaseRepositories;

namespace EV_Station.Infrastructure.Repositories
{
    public class DriverLicenseRepository : GenericRepository<DriverLicense>, IDriverLicenseRepository
    {
        public DriverLicenseRepository(EVStationDbContext context) : base(context)
        {

        }
        public async Task<DriverLicense?> GetDriverLicenseByLinceseNumber(string number)
        {
            return await _dbSet.FindAsync(number);
        }
    }
}
