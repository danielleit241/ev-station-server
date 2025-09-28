using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.Commands;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Domain.Models;
using MediatR;

namespace EV_Station.Application.DriverLisences.CommandHandlers
{
    public class UpdateDriverLicenseHandler : IRequestHandler<UpdateDriverLicense, GenericApiResponse<DriverLicenseResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public UpdateDriverLicenseHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<GenericApiResponse<DriverLicenseResponse>> Handle(UpdateDriverLicense request, CancellationToken cancellationToken)
        {
            var driverLicenseRepository = _uow.DriverLicenses;

            var driverLicense = await driverLicenseRepository.GetByIdAsync(request.id);
            if (driverLicense == null)
            {
                return GenericApiResponse<DriverLicenseResponse>.FailResponse("Bằng lái xe không tồn tại.");
            }

            if (driverLicense.VerificationStatus == Domain.Models.Enums.VerificationStatus.Verified)
            {
                return GenericApiResponse<DriverLicenseResponse>.FailResponse("Không thể cập nhật bằng lái xe đã được phê duyệt.");
            }

            var updatedLicenseNumber = GetUpdatedLicenseCard(driverLicense, _mapper.Map<DriverLicense>(request.dto));

            driverLicenseRepository.Update(driverLicense);
            await _uow.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<DriverLicenseResponse>(driverLicense);
            return GenericApiResponse<DriverLicenseResponse>.SuccessResponse(response, "Cập nhật bằng lái xe thành công.");
        }

        private DriverLicense GetUpdatedLicenseCard(DriverLicense existingLicense, DriverLicense updatedLicense)
        {
            existingLicense.LicenseNumber = updatedLicense.LicenseNumber;
            existingLicense.FullName = updatedLicense.FullName;
            existingLicense.DateOfBirth = updatedLicense.DateOfBirth;
            existingLicense.Nationality = updatedLicense.Nationality;
            existingLicense.Address = updatedLicense.Address;
            existingLicense.LicenseClass = updatedLicense.LicenseClass;
            existingLicense.BeginingDate = updatedLicense.BeginingDate;
            existingLicense.ExpiresDate = updatedLicense.ExpiresDate;
            existingLicense.ClassificationOfMotorVehicles = updatedLicense.ClassificationOfMotorVehicles;
            existingLicense.FrontImagePath = updatedLicense.FrontImagePath;
            existingLicense.BackImagePath = updatedLicense.BackImagePath;

            return existingLicense;
        }
    }
}
