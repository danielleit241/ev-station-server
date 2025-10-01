using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using MediatR;

namespace EV_Station.Application.RentalLocation.Queries
{
    public record GetRentalLocationMarker(Guid Id) : IRequest<GenericApiResponse<LocationResponse>>;
}
