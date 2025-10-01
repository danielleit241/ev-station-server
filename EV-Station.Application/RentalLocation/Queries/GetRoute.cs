using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Dtos.Requests;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using MediatR;

namespace EV_Station.Application.RentalLocation.Queries
{
    public record GetRoute(RouteLocationRequest dto) : IRequest<GenericApiResponse<RouteLocationResponse>>;
}
