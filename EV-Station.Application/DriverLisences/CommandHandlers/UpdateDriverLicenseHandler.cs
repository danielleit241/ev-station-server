using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.Commands;
using EV_Station.Application.DriverLisences.DTOs.Responses;
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

            driverLicense.LicenseNumber = request.dto.LicenseNumber;
            driverLicense.FullName = request.dto.FullName;
            driverLicense.DateOfBirth = request.dto.DateOfBirth;
            driverLicense.Nationality = request.dto.Nationality;
            driverLicense.Address = request.dto.Address;
            driverLicense.LicenseClass = request.dto.LicenseClass;
            driverLicense.BeginingDate = request.dto.BeginingDate;
            driverLicense.ExpiresDate = request.dto.ExpiresDate;
            driverLicense.ClassificationOfMotorVehicles = request.dto.ClassificationOfMotorVehicles;
            driverLicense.FrontImagePath = request.dto.FrontImagePath;
            driverLicense.BackImagePath = request.dto.BackImagePath;
            driverLicense.UserId = request.id;

            driverLicenseRepository.Update(driverLicense);
            await _uow.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<DriverLicenseResponse>(driverLicense);
            return GenericApiResponse<DriverLicenseResponse>.SuccessResponse(response, "Cập nhật bằng lái xe thành công.");
        }
    }
}
