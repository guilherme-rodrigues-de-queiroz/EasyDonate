using AutoMapper;
using EasyDonate.Application.DTOs.Mappings.Extensions;
using EasyDonate.Application.DTOs.Requests.Donors;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.DTOs.Mappings;

public class DonorProfile : Profile
{
    public DonorProfile()
    {
        CreateMap<Donor, DonorResponseDTO>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Donations, opt => opt.MapFrom(src => src.Donations))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.BirthDate)));

        CreateMap<CreateDonorDTO, Donor>()
            .ApplyTrimToStrings();

        CreateMap<UpdateDonorDTO, Donor>()
            .ApplyTrimToStrings()
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember is not null));
    }
}
