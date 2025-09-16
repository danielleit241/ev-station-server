using EV_Station.Domain.Models;

namespace EV_Station.Application.Common.Abstractions.IRepositories
{
    public interface IProviderRepository : IGenericRepository<Provider>
    {
        Task<Provider?> GetProviderByName(string providerName);
    }
}
