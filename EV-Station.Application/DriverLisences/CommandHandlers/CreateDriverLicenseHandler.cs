using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.Commands;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Domain.Models;
using MediatR;

namespace EV_Station.Application.DriverLisences.CommandHandlers
{
    public class CreateDriverLicenseHandler : IRequestHandler<CreateDriverLicense, GenericApiResponse<DriverLicenseResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDriverLicenseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<DriverLicenseResponse>> Handle(CreateDriverLicense request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<DriverLicense>();
            var licenses = await repo.GetAllAsync(u => u.User);
            var driverLicense = _mapper.Map<DriverLicense>(request.dto);

            var hasDriverLicense = licenses.Any(x => x.UserId == request.userId && x.LicenseClass == driverLicense.LicenseClass);
            if (hasDriverLicense)
            {
                return GenericApiResponse<DriverLicenseResponse>.FailResponse($"Người dùng đã có giấy phép lái xe hạng {driverLicense.LicenseClass}.");
            }

            var existingLicenseNumber = licenses.Any(x => x.LicenseNumber == driverLicense.LicenseNumber);
            if (existingLicenseNumber)
            {
                return GenericApiResponse<DriverLicenseResponse>.FailResponse($"Số giấy phép lái xe {driverLicense.LicenseNumber} đã tồn tại.");
            }

            driverLicense.UserId = request.userId;
            repo.Add(driverLicense);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = _mapper.Map<DriverLicenseResponse>(driverLicense);
            return GenericApiResponse<DriverLicenseResponse>.SuccessResponse(response);
        }
    }
}

