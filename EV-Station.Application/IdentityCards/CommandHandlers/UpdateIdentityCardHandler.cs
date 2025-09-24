using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.Commands;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.IdentityCards.CommandHandlers
{
    public class UpdateIdentityCardHandler : IRequestHandler<UpdateIdentityCard, GenericApiResponse<IdentityCardResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public UpdateIdentityCardHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<GenericApiResponse<IdentityCardResponse>> Handle(UpdateIdentityCard request, CancellationToken cancellationToken)
        {
            var identityCardRepository = _uow.IdentityCards;

            var identityCard = await identityCardRepository.GetByIdAsync(request.id);
            if (identityCard == null)
            {
                return GenericApiResponse<IdentityCardResponse>.FailResponse("Thẻ căn cước không tồn tại.");
            }

            identityCard.CardNumber = request.dto.CardNumber;
            identityCard.FullName = request.dto.FullName;
            identityCard.Sex = request.dto.Sex;
            identityCard.Nationality = request.dto.Nationality;
            identityCard.DateOfBirth = request.dto.DateOfBirth;
            identityCard.PlaceOfOrigin = request.dto.PlaceOfOrigin;
            identityCard.PlaceOfResidence = request.dto.PlaceOfResidence;
            identityCard.CreateDate = request.dto.CreateDate;
            identityCard.DayOfExpiry = request.dto.DayOfExpiry;
            identityCard.FrontImagePath = request.dto.FrontImageUrl;
            identityCard.BackImagePath = request.dto.BackImageUrl;

            identityCardRepository.Update(identityCard);
            await _uow.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<IdentityCardResponse>(identityCard);
            return GenericApiResponse<IdentityCardResponse>.SuccessResponse(response, "Cập nhật thẻ căn cước thành công.");
        }
    }
}
