using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;
using MediatR;

namespace EV_Station.Application.DriverLisences.QueryHandlers
{
    public class GetDriverLicenseByLicenseNumberHandler : IRequestHandler<GetDriverLicenseByLicenseNumber, GenericApiResponse<DriverLicenseResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetDriverLicenseByLicenseNumberHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<DriverLicenseResponse>> Handle(GetDriverLicenseByLicenseNumber request, CancellationToken cancellationToken)
        {
            var driverLicenses = await _uow.DriverLicenses.GetAllAsync();
            var driverLicense = driverLicenses.FirstOrDefault(dl => dl.LicenseNumber == request.licenseNumber);
            if (driverLicense is null)
            {
                return GenericApiResponse<DriverLicenseResponse>.FailResponse("Không tìm thấy bằng lái xe nào!");
            }
            var driverLicenseResponse = _mapper.Map<DriverLicenseResponse>(driverLicense);
            return GenericApiResponse<DriverLicenseResponse>.SuccessResponse(driverLicenseResponse, "Lấy bằng lái thành công");
        }
    }
}
