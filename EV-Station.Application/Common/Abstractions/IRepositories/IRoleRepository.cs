using EV_Station.Domain.Models;

namespace EV_Station.Application.Common.Abstractions.IRepositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetRoleByName(string roleName);
    }
}
