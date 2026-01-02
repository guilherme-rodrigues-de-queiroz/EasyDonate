using EasyDonate.Application.DTOs.Requests.Auth;
using EasyDonate.Application.DTOs.Responses;

namespace EasyDonate.Application.Interfaces.Auth;

public interface IAuthService
{
    public Task<ResponseModel<TokenResponseDTO?>> LoginAsync(LoginDto dto);
}