using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.DriverLisences.Queries
{
    public record GetDriverLicenseById(string lisenceNumber) : IRequest<GenericApiResponse<DriverLicenseScanResponse>>;
}
