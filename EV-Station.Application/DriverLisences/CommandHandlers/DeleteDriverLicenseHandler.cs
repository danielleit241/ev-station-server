using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.Commands;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.DriverLisences.CommandHandlers
{
    public class DeleteDriverLicenseHandler : IRequestHandler<DeleteDriverLicense, GenericApiResponse<DriverLicenseResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public DeleteDriverLicenseHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<GenericApiResponse<DriverLicenseResponse>> Handle(DeleteDriverLicense request, CancellationToken cancellationToken)
        {
            var driverLicenseRepository = _uow.DriverLicenses;

            var driverLicenses = await driverLicenseRepository.GetAllAsync();
            var driverLicense = driverLicenses.FirstOrDefault(dl => dl.LicenseNumber == request.licenseNumber);

            if (driverLicense == null)
            {
                return GenericApiResponse<DriverLicenseResponse>.FailResponse("Driver license not found for the specified user.");
            }

            driverLicenseRepository.Delete(driverLicense);
            await _uow.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<DriverLicenseResponse>(driverLicense);
            return GenericApiResponse<DriverLicenseResponse>.SuccessResponse(response, "Driver license deleted successfully.");
        }
    }
}
