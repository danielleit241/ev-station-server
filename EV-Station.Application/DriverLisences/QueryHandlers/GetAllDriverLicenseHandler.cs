using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;
using MediatR;

namespace EV_Station.Application.DriverLisences.QueryHandlers
{
    public class GetAllDriverLicenseHandler : IRequestHandler<GetAllDriverLicense, GenericApiResponse<ICollection<DriverLicenseResponse>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllDriverLicenseHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<ICollection<DriverLicenseResponse>>> Handle(GetAllDriverLicense request, CancellationToken cancellationToken)
        {
            var driverLicenses = await _uow.DriverLicenses.GetAllAsync();
            if (driverLicenses is null || !driverLicenses.Any())
            {
                return GenericApiResponse<ICollection<DriverLicenseResponse>>.FailResponse("Không tìm thấy bằng lái xe nào");
            }
            var driverLicenseResponses = _mapper.Map<ICollection<DriverLicenseResponse>>(driverLicenses);
            return GenericApiResponse<ICollection<DriverLicenseResponse>>.SuccessResponse(driverLicenseResponses, "Lấy danh sách bằng lái xe thành công");
        }
    }
}