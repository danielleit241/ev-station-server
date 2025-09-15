using AutoMapper;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Requests;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Domain.Models;

namespace EV_Station.Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDto>();
            CreateMap<RegisterUser, User>()
                .ForMember(d => d.PasswordHash, otp => otp.Ignore())
                .ForMember(d => d.Provider, otp => otp.MapFrom(_ => "Local"));
            CreateMap<GoogleLoginDto, User>()
                .ForMember(d => d.PasswordHash, otp => otp.Ignore())
                .ForMember(d => d.Provider, otp => otp.MapFrom(_ => "Google"));
        }
    }
}
