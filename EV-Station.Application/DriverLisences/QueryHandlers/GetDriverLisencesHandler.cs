using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;
using MediatR;

namespace EV_Station.Application.DriverLisences.QueryHandlers
{
    public class GetDriverLisencesHandler : IRequestHandler<GetDriverLicense, GenericApiResponse<ICollection<DriverLicenseScanResponse>>>
    {
        public Task<GenericApiResponse<ICollection<DriverLicenseScanResponse>>> Handle(GetDriverLicense request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
