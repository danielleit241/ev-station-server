using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.Commands;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.IdentityCards.CommandHandlers
{
    public class DeleteIdentityCardHandle : IRequestHandler<DeleteIdentityCard, GenericApiResponse<IdentityCardResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public DeleteIdentityCardHandle(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<IdentityCardResponse>> Handle(DeleteIdentityCard request, CancellationToken cancellationToken)
        {
            var identityCardRepository = _uow.IdentityCards;
            var identityCard = await identityCardRepository.GetByIdAsync(request.id);
            if (identityCard is null)
            {
                return GenericApiResponse<IdentityCardResponse>.FailResponse("Identitycard card not found!");
            }
            identityCardRepository.Delete(identityCard);
            await _uow.SaveChangesAsync(cancellationToken);

            var identityCardResponse = _mapper.Map<IdentityCardResponse>(identityCard);
            return GenericApiResponse<IdentityCardResponse>.SuccessResponse(identityCardResponse, "Delete identity card successfully");
        }
    }
}
