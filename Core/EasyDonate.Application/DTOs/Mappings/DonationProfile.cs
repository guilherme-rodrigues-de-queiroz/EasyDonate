using AutoMapper;
using EasyDonate.Application.DTOs.Mappings.Extensions;
using EasyDonate.Application.DTOs.Requests.Donation;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.DTOs.Mappings;

public class DonationProfile : Profile
{
    public DonationProfile()
    {
        CreateMap<CreateDonationDTO, Donation>()
            .ApplyTrimToStrings();

        CreateMap<CreateDonationDTO, Appointment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ApplyTrimToStrings();

        CreateMap<Donation, DonationResponseDTO>()
            .ForMember(dest => dest.Appointment, opt => opt.MapFrom(src => src.Appointment))
            .ForMember(dest => dest.FoodExpirationDate, opt =>
                opt.MapFrom(src => src.FoodExpirationDate.HasValue ? DateOnly.FromDateTime(src.FoodExpirationDate.Value) : (DateOnly?)null));
    }
}
