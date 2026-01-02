using AutoMapper;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.DTOs.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<User, TokenResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email ?? string.Empty))
            .ForMember(dest => dest.AccessToken, opt => opt.Ignore())
            .ForMember(dest => dest.ExpiresIn, opt => opt.Ignore())
            .ForMember(dest => dest.TokenType, opt => opt.Ignore());
    }
}
