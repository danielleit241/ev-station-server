using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Commands;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using MediatR;

namespace EV_Station.Application.RentalLocation.CommandHandlers
{
    public class CreateRentalLocationHandler : IRequestHandler<CreateRentalLocation, GenericApiResponse<RentalLocationResponse>>
    {
        private readonly IGenericRepository<Domain.Models.RentalLocation> _rentalLocationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateRentalLocationHandler(IGenericRepository<Domain.Models.RentalLocation> rentalLocationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rentalLocationRepository = rentalLocationRepository;
        }

        public async Task<GenericApiResponse<RentalLocationResponse>> Handle(CreateRentalLocation request, CancellationToken cancellationToken)
        {
            var rentalLocation = _mapper.Map<Domain.Models.RentalLocation>(request.dto);
            _rentalLocationRepository.Add(rentalLocation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = _mapper.Map<RentalLocationResponse>(rentalLocation);
            return GenericApiResponse<RentalLocationResponse>.SuccessResponse(response, "Rental location created successfully.");
        }
    }
}
