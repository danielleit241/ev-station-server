using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.Commands;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.DriverLisences.CommandHandlers
{
    public class CreateDriverLicenseHandler : IRequestHandler<CreateDriverLisence, GenericApiResponse<DriverLicenseResponse>>
    {
        public Task<GenericApiResponse<DriverLicenseResponse>> Handle(CreateDriverLisence request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
