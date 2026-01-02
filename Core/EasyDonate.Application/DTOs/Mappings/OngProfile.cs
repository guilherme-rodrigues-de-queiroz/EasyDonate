using AutoMapper;
using EasyDonate.Application.DTOs.Mappings.Extensions;
using EasyDonate.Application.DTOs.Requests.Ongs;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.DTOs.Mappings;

public class OngProfile : Profile
{
    public OngProfile()
    {
        CreateMap<Ong, OngResponseDTO>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Donations, opt => opt.MapFrom(src => src.Donations));

        CreateMap<CreateOngDTO, Ong>()
            .ApplyTrimToStrings();

        CreateMap<UpdateOngDTO, Ong>()
            .ApplyTrimToStrings()
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember is not null));
    }
}
