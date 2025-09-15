using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.CommandHandlers
{

    public class GoogleLoginUserHandler : IRequestHandler<GoogleLoginUser, GenericApiResponse<UserTokensReponse>>
    {

        Task<GenericApiResponse<UserTokensReponse>> IRequestHandler<GoogleLoginUser, GenericApiResponse<UserTokensReponse>>.Handle(GoogleLoginUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
