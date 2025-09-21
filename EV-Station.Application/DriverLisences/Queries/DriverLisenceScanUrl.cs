using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Requests;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.DriverLisences.Queries
{
    public record DriverLisenceScanUrl(DriverLisenceScanUrlRequest dto) : IRequest<GenericApiResponse<DriverLisenceScanResponse>>;
}
