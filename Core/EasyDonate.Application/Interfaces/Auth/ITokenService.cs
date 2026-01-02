using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Interfaces.Auth;

public interface ITokenService
{
    TokenResultDTO GenerateToken(User user);
}
