using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Domain.Models;
using EV_Station.Infrastructure.Persistence.SqlServer.Data;
using EV_Station.Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EV_Station.Infrastructure.Repositories
{
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(EVStationDbContext context) : base(context)
        {
        }

        public async Task<Provider?> GetProviderByName(string providerName)
        {
            return await _dbSet.Where(r => r.Name == providerName).FirstOrDefaultAsync();
        }
    }
}
