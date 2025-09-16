using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Domain.Models;
using EV_Station.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EV_Station.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EVStationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbSet.Include(r => r.Role).FirstAsync(u => u.Email == email.ToLower().Trim());
        }

        public async Task<bool> IsEmailExist(string email) => await _dbSet.AnyAsync(u => u.Email == email.ToLower().Trim());

    }
}
