using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using EV_Station.Application.RentalLocation.Queries;
using MediatR;

namespace EV_Station.Application.RentalLocation.QueryHandlers
{
    public class GetRentalLocationByIdHandler : IRequestHandler<GetRentalLocationById, GenericApiResponse<RentalLocationResponse>>
    {
        private readonly IGenericRepository<Domain.Models.RentalLocation> _rentalLocationRepository;
        private readonly IMapper _mapper;

        public GetRentalLocationByIdHandler(IGenericRepository<Domain.Models.RentalLocation> rentalLocationRepository, IMapper mapper)
        {
            _rentalLocationRepository = rentalLocationRepository;
            _mapper = mapper;
        }
        public async Task<GenericApiResponse<RentalLocationResponse>> Handle(GetRentalLocationById request, CancellationToken cancellationToken)
        {
            var rentalLocation = await _rentalLocationRepository.GetByIdAsync(request.id);
            if (rentalLocation == null)
            {
                return GenericApiResponse<RentalLocationResponse>.FailResponse("Rental location not found.");
            }
            var response = _mapper.Map<RentalLocationResponse>(rentalLocation);
            return GenericApiResponse<RentalLocationResponse>.SuccessResponse(response, "Rental location retrieved successfully.");
        }
    }
}
