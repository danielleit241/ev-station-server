using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Application.IdentityCards.Queries;
using EV_Station.Domain.Models;
using MediatR;

namespace EV_Station.Application.IdentityCards.QueryHandlers
{
    public class GetAllIdentityCardsHandler : IRequestHandler<GetAllIdentityCards, GenericApiResponse<ICollection<IdentityCardResponse>>>
    {
        private readonly IGenericRepository<IdentityCard> _identityCardRepository;
        private readonly IMapper _mapper;
        public GetAllIdentityCardsHandler(IGenericRepository<IdentityCard> identityCardRepository, IMapper mapper)
        {
            _identityCardRepository = identityCardRepository;
            _mapper = mapper;
        }
        public async Task<GenericApiResponse<ICollection<IdentityCardResponse>>> Handle(GetAllIdentityCards request, CancellationToken cancellationToken)
        {
            var identityCards = await _identityCardRepository.GetAllAsync();
            if (identityCards is null || !identityCards.Any())
            {
                return GenericApiResponse<ICollection<IdentityCardResponse>>.FailResponse("No identity cards found");
            }
            var identityCardsResponse = _mapper.Map<ICollection<IdentityCardResponse>>(identityCards);

            return GenericApiResponse<ICollection<IdentityCardResponse>>.SuccessResponse(identityCardsResponse);
        }
    }
}
