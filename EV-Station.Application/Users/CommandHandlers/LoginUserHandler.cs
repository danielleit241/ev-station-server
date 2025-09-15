using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.CommandHandlers
{
    public class LoginUserHandler : IRequestHandler<LoginUser, UserTokensReponse?>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LoginUserHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public Task<UserTokensReponse?> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
