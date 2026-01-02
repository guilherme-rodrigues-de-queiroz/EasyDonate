using AutoMapper;
using EasyDonate.Application.DTOs.Mappings.Extensions;
using EasyDonate.Application.DTOs.Requests.Donors;
using EasyDonate.Application.DTOs.Requests.Ongs;
using EasyDonate.Application.DTOs.Requests.Users;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.DTOs.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDTO>();

        CreateMap<CreateUserDTO, User>()
            .ApplyTrimToStrings();

        CreateMap<CreateDonorDTO, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ApplyTrimToStrings();

        CreateMap<CreateOngDTO, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ApplyTrimToStrings();

        CreateMap<UpdateUserDTO, User>()
            .ApplyTrimToStrings()
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember is not null));
    }
}
