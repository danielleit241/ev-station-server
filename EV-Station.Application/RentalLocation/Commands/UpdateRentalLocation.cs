using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Dtos.Requests;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using MediatR;

namespace EV_Station.Application.RentalLocation.Commands
{
    public record UpdateRentalLocation(Guid id, RentalLocationRequest dto) : IRequest<GenericApiResponse<RentalLocationResponse>>;
}
