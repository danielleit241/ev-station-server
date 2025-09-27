using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.Commands;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Domain.Models;
using MediatR;

namespace EV_Station.Application.IdentityCards.CommandHandlers
{
    public class CreateIdentityCardHandler : IRequestHandler<CreateIdentityCard, GenericApiResponse<IdentityCardResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateIdentityCardHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<IdentityCardResponse>> Handle(CreateIdentityCard request, CancellationToken cancellationToken)
        {
            var identityCardRepository = _uow.IdentityCards;
            var identityCards = await identityCardRepository.GetAllAsync();

            var userHasIdentityCard = identityCards.Any(ic => ic.UserId == request.userId);

            if (userHasIdentityCard)
            {
                return GenericApiResponse<IdentityCardResponse>.FailResponse("Người dùng đã có thẻ căn cước.");
            }

            var existingIdentityCard = identityCards.Any(ic => ic.CardNumber == request.dto.CardNumber);

            if (existingIdentityCard)
            {
                return GenericApiResponse<IdentityCardResponse>.FailResponse("Số thẻ căn cước đã tồn tại");
            }

            var newIdentityCard = _mapper.Map<IdentityCard>(request.dto);

            newIdentityCard.CreatedAt = DateTime.UtcNow;
            newIdentityCard.UserId = request.userId;

            identityCardRepository.Add(newIdentityCard);
            await _uow.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<IdentityCardResponse>(newIdentityCard);
            return GenericApiResponse<IdentityCardResponse>.SuccessResponse(response);
        }
    }
}
