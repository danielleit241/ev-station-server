using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;
using MediatR;

namespace EV_Station.Application.DriverLisences.QueryHandlers
{
    public class GetListDriverLicenseByUserIdHandler : IRequestHandler<GetListDriverLicenseByUserId, GenericApiResponse<ICollection<DriverLicenseResponse>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetListDriverLicenseByUserIdHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<ICollection<DriverLicenseResponse>>> Handle(GetListDriverLicenseByUserId request, CancellationToken cancellationToken)
        {
            var driverLicenses = await _uow.DriverLicenses.GetAllAsync();
            var driverLicenseList = driverLicenses.Where(dl => dl.UserId == request.id).ToList();
            if (driverLicenseList is null || driverLicenseList.Count == 0)
            {
                return GenericApiResponse<ICollection<DriverLicenseResponse>>.FailResponse("Không tìm thấy bằng lái xe nào");
            }
            var driverLicenseResponseList = _mapper.Map<ICollection<DriverLicenseResponse>>(driverLicenseList);
            return GenericApiResponse<ICollection<DriverLicenseResponse>>.SuccessResponse(driverLicenseResponseList);
        }
    }
}
