using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;
using MediatR;

namespace EV_Station.Application.DriverLisences.QueryHandlers
{
    public class GetDriverLisencesHandler : IRequestHandler<GetDriverLisences, GenericApiResponse<ICollection<DriverLisenceScanResponse>>>
    {
        public Task<GenericApiResponse<ICollection<DriverLisenceScanResponse>>> Handle(GetDriverLisences request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
