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
            CreateMap<IdentityCardRequest, IdentityCard>()
                .ForMember(d => d.DateOfBirth,
                            opt => opt.MapFrom(s => DateOnly.FromDateTime(s.DateOfBirth.ToDateTime(TimeOnly.MinValue))))
                .ForMember(d => d.CreateDate,
                            opt => opt.MapFrom(s => DateOnly.FromDateTime(s.CreateDate.ToDateTime(TimeOnly.MinValue))))
                .ForMember(d => d.DayOfExpiry,
                            opt => opt.MapFrom(s => DateOnly.FromDateTime(s.DayOfExpiry.ToDateTime(TimeOnly.MinValue))))
                .ForMember(d => d.CreatedAt,
                            opt => opt.Ignore());

            CreateMap<IdentityCard, IdentityCardResponse>()
                .ForMember(d => d.DateOfBirth,
                           opt => opt.MapFrom(s => DateOnly.FromDateTime(s.DateOfBirth)))
                .ForMember(d => d.CreateDate,
                           opt => opt.MapFrom(s => DateOnly.FromDateTime(s.CreateDate)))
                .ForMember(d => d.DayOfExpiry,
                           opt => opt.MapFrom(s => DateOnly.FromDateTime(s.DayOfExpiry)));

        }
    }
}
