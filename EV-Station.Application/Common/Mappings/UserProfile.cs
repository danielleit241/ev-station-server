using AutoMapper;
using EV_Station.Application.Users.DTOs.Requests;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Domain.Models;

namespace EV_Station.Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDto>()
                .ForMember(d => d.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : null));

            CreateMap<RegisterUserDto, User>()
                .ForMember(d => d.PasswordHash, opt => opt.Ignore());

            CreateMap<GoogleLoginDto, User>()
                .ForMember(d => d.PasswordHash, otp => otp.Ignore());

            CreateMap<CreateUserDto, User>()
                .ForMember(d => d.PasswordHash, otp => otp.Ignore());
        }
    }
}
