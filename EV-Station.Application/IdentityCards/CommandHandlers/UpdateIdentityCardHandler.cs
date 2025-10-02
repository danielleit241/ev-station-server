using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.Commands;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Domain.Models;
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
            var identityCard = await identityCardRepository.GetByUserIdAsync(request.id);
            if (identityCard == null)
            {
                return GenericApiResponse<IdentityCardResponse>.FailResponse("Thẻ căn cước không tồn tại.");
            }

            if (identityCard.Status == Domain.Models.Enums.VerificationStatus.Verified)
            {
                return GenericApiResponse<IdentityCardResponse>.FailResponse("Không thể cập nhật thẻ căn cước đã được phê duyệt.");
            }

            var updatedIdentityCard = GetUpdateIdentityCard(identityCard, _mapper.Map<IdentityCard>(request.dto));
            identityCardRepository.Update(updatedIdentityCard);
            await _uow.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<IdentityCardResponse>(identityCard);
            return GenericApiResponse<IdentityCardResponse>.SuccessResponse(response, "Cập nhật thẻ căn cước thành công.");
        }

        private IdentityCard GetUpdateIdentityCard(IdentityCard existingCard, IdentityCard updatedCard)
        {
            existingCard.FullName = updatedCard.FullName;
            existingCard.Sex = updatedCard.Sex;
            existingCard.Nationality = updatedCard.Nationality;
            existingCard.DateOfBirth = updatedCard.DateOfBirth;
            existingCard.CreateDate = updatedCard.CreateDate;
            existingCard.DayOfExpiry = updatedCard.DayOfExpiry;
            existingCard.PlaceOfOrigin = updatedCard.PlaceOfOrigin;
            existingCard.PlaceOfResidence = updatedCard.PlaceOfResidence;
            existingCard.FrontImagePath = updatedCard.FrontImagePath;
            existingCard.BackImagePath = updatedCard.BackImagePath;
            existingCard.Status = Domain.Models.Enums.VerificationStatus.Pending;

            return existingCard;
        }
    }
}
