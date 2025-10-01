using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using EV_Station.Application.RentalLocation.Queries;
using MediatR;

namespace EV_Station.Application.RentalLocation.QueryHandlers
{
    public class GetRentalLocationMarkerHandler : IRequestHandler<GetRentalLocationMarker, GenericApiResponse<LocationResponse>>
    {
        private readonly IGeocodingService _geocodingService;
        private readonly IGenericRepository<EV_Station.Domain.Models.RentalLocation> _rentalLocationRepository;

        public GetRentalLocationMarkerHandler(IGeocodingService geocodingService, IGenericRepository<EV_Station.Domain.Models.RentalLocation> rentalLocationRepository)
        {
            _geocodingService = geocodingService;
            _rentalLocationRepository = rentalLocationRepository;
        }

        public async Task<GenericApiResponse<LocationResponse>> Handle(GetRentalLocationMarker request, CancellationToken cancellationToken)
        {
            var location = await _rentalLocationRepository.GetByIdAsync(request.Id);
            if (location == null)
            {
                return GenericApiResponse<LocationResponse>.FailResponse("Rental location not found.");
            }
            var address = await _geocodingService.GetCoordinatesAsync(location.Address);
            return GenericApiResponse<LocationResponse>.SuccessResponse(address);
        }
    }
}
