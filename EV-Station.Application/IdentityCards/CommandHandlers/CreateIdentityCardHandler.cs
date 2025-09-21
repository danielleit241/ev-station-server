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
            await _uow.BeginTransactionAsync();
            try
            {
                var identityCardRepository = _uow.IdentityCards;
                var userRepository = _uow.Users;

                var identityCards = await identityCardRepository.GetAllAsync();
                var userHasIdentityCard = identityCards.Any(ic => ic.UserId == request.userId);

                if (userHasIdentityCard)
                {
                    return GenericApiResponse<IdentityCardResponse>.FailResponse("User already has an identity card.");
                }

                var existingIdentityCard = await identityCardRepository.GetIdentityCardByNumber(request.dto.CardNumber);

                if (existingIdentityCard != null)
                {
                    return GenericApiResponse<IdentityCardResponse>.FailResponse("User already owns this card number.");
                }

                var newIdentityCard = _mapper.Map<IdentityCard>(request.dto);
                newIdentityCard.UserId = request.userId;
                await identityCardRepository.AddAsync(newIdentityCard);

                await _uow.SaveChangesAsync(cancellationToken);
                await _uow.CommitAsync();
                var response = _mapper.Map<IdentityCardResponse>(newIdentityCard);
                return GenericApiResponse<IdentityCardResponse>.SuccessResponse(response);
            }
            catch
            {
                await _uow.RollbackAsync();
                return GenericApiResponse<IdentityCardResponse>.FailResponse("An error occurred while creating the identity card.");
            }

        }
    }
}
