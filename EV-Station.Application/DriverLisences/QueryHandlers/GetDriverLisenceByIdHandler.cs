using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;
using MediatR;

namespace EV_Station.Application.DriverLisences.QueryHandlers
{
    public class GetDriverLisenceByIdHandler : IRequestHandler<GetDriverLicenseById, GenericApiResponse<DriverLicenseScanResponse>>
    {
        public Task<GenericApiResponse<DriverLicenseScanResponse>> Handle(GetDriverLicenseById request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
