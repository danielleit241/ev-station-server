using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Domain.Models;

namespace EV_Station.Application.Common.Abstractions.IRepositories
{
    public interface IDriverLicenseRepository : IGenericRepository<DriverLicense>
    {
        Task<DriverLicense?> GetDriverLicenseByLinceseNumber(string number);
    }
}
