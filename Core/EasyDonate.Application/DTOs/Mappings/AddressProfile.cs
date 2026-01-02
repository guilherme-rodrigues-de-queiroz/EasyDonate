using AutoMapper;
using EasyDonate.Application.DTOs.Mappings.Extensions;
using EasyDonate.Application.DTOs.Requests.Addresses;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.DTOs.Mappings;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressResponseDTO>();

        CreateMap<CreateAddressDTO, Address>()
            .ForMember(dest => dest.Ong, opt => opt.Ignore())
            .ForMember(dest => dest.Donor, opt => opt.Ignore())
            .ApplyTrimToStrings();
    }
}
