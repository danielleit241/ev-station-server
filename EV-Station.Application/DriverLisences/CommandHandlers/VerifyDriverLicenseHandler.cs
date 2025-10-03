using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.Commands;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.DriverLisences.CommandHandlers
{
    public class VerifyDriverLicenseHandler : IRequestHandler<VerifyDriverLicense, GenericApiResponse<DriverLicenseResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public VerifyDriverLicenseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GenericApiResponse<DriverLicenseResponse>> Handle(VerifyDriverLicense request, CancellationToken cancellationToken)
        {
            var driverLicenseRepo = _uow.DriverLicenses;
            var driverLicense = await driverLicenseRepo.GetDriverLicenseByLinceseNumber(request.licenseNumber);
            if (driverLicense == null)
            {
                return GenericApiResponse<DriverLicenseResponse>.FailResponse("Driver license not found");
            }
            if (driverLicense.VerificationStatus == Domain.Models.Enums.VerificationStatus.Verified)
            {
                return GenericApiResponse<DriverLicenseResponse>.FailResponse("Driver license is already verified");
            }
            driverLicense.VerificationStatus = request.status.ToLower() == "verified" ? Domain.Models.Enums.VerificationStatus.Verified : Domain.Models.Enums.VerificationStatus.Rejected;
            driverLicenseRepo.Update(driverLicense);
            await _uow.SaveChangesAsync(cancellationToken);
            var driverLicenseResponse = _mapper.Map<DriverLicenseResponse>(driverLicense);
            return GenericApiResponse<DriverLicenseResponse>.SuccessResponse(driverLicenseResponse, "Driver license verification status updated successfully");
        }
    }
}
