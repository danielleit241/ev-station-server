using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.DriverLisences.Commands
{
    public record DeleteDriverLicense(string licenseNumber) : IRequest<GenericApiResponse<DriverLicenseResponse>>;
}
