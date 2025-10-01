using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using EV_Station.Application.RentalLocation.Queries;
using MediatR;

namespace EV_Station.Application.RentalLocation.QueryHandlers
{
    public class GetAllRentalLocationHandler : IRequestHandler<GetAllRentalLocation, GenericApiResponse<IEnumerable<RentalLocationResponse>>>
    {
        private readonly IGenericRepository<Domain.Models.RentalLocation> _rentalLocationRepository;
        private readonly IMapper _mapper;

        public GetAllRentalLocationHandler(IGenericRepository<Domain.Models.RentalLocation> rentalLocationRepository, IMapper mapper)
        {
            _mapper = mapper;
            _rentalLocationRepository = rentalLocationRepository;
        }

        public async Task<GenericApiResponse<IEnumerable<RentalLocationResponse>>> Handle(GetAllRentalLocation request, CancellationToken cancellationToken)
        {
            var rentalLocations = await _rentalLocationRepository.GetAllAsync();
            if (rentalLocations == null || !rentalLocations.Any())
            {
                return GenericApiResponse<IEnumerable<RentalLocationResponse>>.FailResponse("No rental locations found.");
            }
            var response = _mapper.Map<IEnumerable<RentalLocationResponse>>(rentalLocations);
            return GenericApiResponse<IEnumerable<RentalLocationResponse>>.SuccessResponse(response, "Rental locations retrieved successfully.");
        }
    }
}
