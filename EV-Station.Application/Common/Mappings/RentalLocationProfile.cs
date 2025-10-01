using AutoMapper;
using EV_Station.Application.RentalLocation.Dtos.Requests;
using EV_Station.Application.RentalLocation.Dtos.Responses;

namespace EV_Station.Application.Common.Mappings
{
    public class RentalLocationProfile : Profile
    {
        public RentalLocationProfile()
        {
            CreateMap<CreateRentalLocationRequest, Domain.Models.RentalLocation>()
                .ForMember(dest => dest.LocationID, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<UpdateRentalLocationRequest, Domain.Models.RentalLocation>();

            CreateMap<Domain.Models.RentalLocation, RentalLocationResponse>();
        }
    }
}
