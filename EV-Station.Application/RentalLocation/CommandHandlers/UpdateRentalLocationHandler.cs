using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Commands;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using MediatR;

namespace EV_Station.Application.RentalLocation.CommandHandlers
{
    public class UpdateRentalLocationHandler : IRequestHandler<UpdateRentalLocation, GenericApiResponse<RentalLocationResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public UpdateRentalLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GenericApiResponse<RentalLocationResponse>> Handle(UpdateRentalLocation request, CancellationToken cancellationToken)
        {
            var rentalLocationRepository = _unitOfWork.Repository<Domain.Models.RentalLocation>();
            var existingRentalLocation = await rentalLocationRepository.GetByIdAsync(request.id);
            if (existingRentalLocation == null)
            {
                return GenericApiResponse<RentalLocationResponse>.FailResponse("Rental location not found");
            }
            existingRentalLocation.Name = request.dto.Name;
            existingRentalLocation.Address = request.dto.Address;
            existingRentalLocation.Phone = request.dto.Phone;
            existingRentalLocation.Email = request.dto.Email;
            existingRentalLocation.ManagerName = request.dto.ManagerName;
            existingRentalLocation.OpenHour = request.dto.OpenHour;
            existingRentalLocation.CloseHour = request.dto.CloseHour;
            rentalLocationRepository.Update(existingRentalLocation);
            await _unitOfWork.SaveChangesAsync();
            var rentalLocationResponse = _mapper.Map<RentalLocationResponse>(existingRentalLocation);
            return GenericApiResponse<RentalLocationResponse>.SuccessResponse(rentalLocationResponse, "Rental location updated successfully");
        }

    }
}
