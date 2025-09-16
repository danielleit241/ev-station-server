using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Domain.Models;
using EV_Station.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EV_Station.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(EVStationDbContext context) : base(context)
        {
        }

        public async Task<Role?> GetRoleByName(string roleName)
        {
            return await _dbSet.Where(r => r.Name == roleName).FirstOrDefaultAsync();
        }
    }
}
