using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Commands;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using MediatR;

namespace EV_Station.Application.RentalLocation.CommandHandlers
{
    public class DeleteRentalLocationHandler : IRequestHandler<DeleteRentalLocation, GenericApiResponse<RentalLocationResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteRentalLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GenericApiResponse<RentalLocationResponse>> Handle(DeleteRentalLocation request, CancellationToken cancellationToken)
        {
            var rentalLocationRepository = _unitOfWork.Repository<Domain.Models.RentalLocation>();
            var rentalLocation = await rentalLocationRepository.GetByIdAsync(request.Id);
            if (rentalLocation == null)
            {
                return GenericApiResponse<RentalLocationResponse>.FailResponse("Rental location not found");
            }
            rentalLocationRepository.Delete(rentalLocation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var rentalLocationResponse = _mapper.Map<RentalLocationResponse>(rentalLocation);
            return GenericApiResponse<RentalLocationResponse>.SuccessResponse(rentalLocationResponse, "Rental location deleted successfully");
        }
    }
}
