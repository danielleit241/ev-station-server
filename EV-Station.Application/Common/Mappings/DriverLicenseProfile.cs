using AutoMapper;
using EV_Station.Application.DriverLisences.DTOs.Requests;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Domain.Models;

namespace EV_Station.Application.Common.Mappings
{
    public class DriverLicenseProfile : Profile
    {
        public DriverLicenseProfile()
        {
            CreateMap<DriverLicenseRequest, DriverLicense>()
                .ForMember(d => d.DateOfBirth, opt => opt.MapFrom(s => DateOnly.FromDateTime(s.DateOfBirth.ToDateTime(TimeOnly.MinValue))))
                .ForMember(d => d.BeginingDate, opt => opt.MapFrom(s => DateOnly.FromDateTime(s.BeginingDate.ToDateTime(TimeOnly.MinValue))))
                .ForMember(d => d.ExpiresDate, opt => opt.MapFrom(s => DateOnly.FromDateTime(s.ExpiresDate.ToDateTime(TimeOnly.MinValue))))
                .ForMember(d => d.CreatedAt, opt => opt.Ignore());

            CreateMap<DriverLicense, DriverLicenseResponse>()
                .ForMember(d => d.DateOfBirth, opt => opt.MapFrom(s => DateOnly.FromDateTime(s.DateOfBirth)))
                .ForMember(d => d.BeginingDate, opt => opt.MapFrom(s => DateOnly.FromDateTime(s.BeginingDate)))
                .ForMember(d => d.ExpiresDate, opt => opt.MapFrom(s => DateOnly.FromDateTime(s.ExpiresDate ?? DateTime.MinValue)));
        }
    }
}
