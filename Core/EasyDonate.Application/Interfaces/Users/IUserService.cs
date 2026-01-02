using EasyDonate.Application.DTOs.Requests.Users;
using EasyDonate.Application.DTOs.Responses;

namespace EasyDonate.Application.Interfaces.Users;

public interface IUserService
{
    public Task<ResponseModel<UserResponseDTO?>> InativeUserAsync(string email, UpdateUserDTO dto);
    public Task<ResponseModel<UserResponseDTO?>> ActivateUserAsync(string email, UpdateUserDTO dto);
}