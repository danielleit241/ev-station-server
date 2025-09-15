using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Domain.Models;
using EV_Station.Infrastructure.Persistence.Data;

namespace EV_Station.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EVStationDbContext context) : base(context)
        {
        }
    }
}
