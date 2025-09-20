using AutoMapper;
using EV_Station.Application.IdentityCards.DTOs.Requests;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Domain.Models;

namespace EV_Station.Application.Common.Mappings
{
    public class IdentityCardProfile : Profile
    {
        public IdentityCardProfile()
        {
            CreateMap<IdentityCardRequest, IdentityCard>();

            CreateMap<IdentityCard, IdentityCardResponse>();
        }
    }
}
