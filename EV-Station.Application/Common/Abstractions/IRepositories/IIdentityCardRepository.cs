using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Domain.Models;

namespace EV_Station.Application.Common.Abstractions.IRepositories
{
    public interface IIdentityCardRepository : IGenericRepository<IdentityCard>
    {
        Task<IdentityCard?> GetIdentityCardByNumber(string number);
    }
}
