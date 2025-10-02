using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Domain.Models;
using EV_Station.Infrastructure.Persistence.SqlServer.Data;
using EV_Station.Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EV_Station.Infrastructure.Repositories
{
    public class IdentityCardRepository : GenericRepository<IdentityCard>, IIdentityCardRepository
    {
        public IdentityCardRepository(EVStationDbContext context) : base(context)
        {
        }

        public async Task<IdentityCard?> GetIdentityCardByNumber(string number)
        {
            return await _dbSet.FindAsync(number);
        }

        public async Task<IdentityCard?> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet.FirstOrDefaultAsync(ic => ic.UserId == userId);
        }
    }
}
