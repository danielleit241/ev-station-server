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
            // CreateMap<Source, Destination>();
            CreateMap<DriverLicenseRequest, DriverLicense>()
                .ForMember(d => d.CreatedAt, opt => opt.Ignore());

            CreateMap<DriverLicense, DriverLicenseResponse>();
        }
    }
}
