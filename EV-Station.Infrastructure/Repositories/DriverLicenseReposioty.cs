using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Domain.Models;
using EV_Station.Infrastructure.Persistence.SqlServer.Data;
using EV_Station.Infrastructure.Repositories.BaseRepositories;

namespace EV_Station.Infrastructure.Repositories
{
    public class DriverLicenseReposioty : GenericRepository<DriverLicense>, IDriverLicenseReposioty
    {
        public DriverLicenseReposioty(EVStationDbContext context) : base(context)
        {
        }
    }
}
