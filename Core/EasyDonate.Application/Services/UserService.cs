using AutoMapper;
using EasyDonate.Application.DTOs.Requests.Users;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces.Users;

namespace EasyDonate.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, IUserRepository repository)
    {
        _mapper = mapper;
        _userRepository = repository;
    }

    public async Task<ResponseModel<UserResponseDTO?>> InativeUserAsync(string email, UpdateUserDTO dto)
    {
        try
        {
            var userEntity = await _userRepository.GetUserByEmailAsync(email.ToLower());
            if (userEntity is null) return ResponseModel<UserResponseDTO>.NotFound("Nenhum usuário encontrado.");
            if (!userEntity.Active) return ResponseModel<UserResponseDTO>.Conflict("Usuário já está inativo.");

            userEntity.Active = false;
            userEntity.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateUserAsync(userEntity);
            var userDto = _mapper.Map<UserResponseDTO>(userEntity);
            return ResponseModel<UserResponseDTO?>.Ok(userDto, "Usuário inativado com sucesso.");
        }
        catch (Exception ex)
        {
            return ResponseModel<UserResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<UserResponseDTO?>> ActivateUserAsync(string email, UpdateUserDTO dto)
    {
        try
        {
            var userEntity = await _userRepository.GetUserByEmailAsync(email.ToLower());
            if (userEntity is null) return ResponseModel<UserResponseDTO>.NotFound("Nenhum usuário encontrado.");
            if (userEntity.Active) return ResponseModel<UserResponseDTO>.Conflict("Usuário já está ativo.");

            userEntity.Active = true;
            userEntity.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateUserAsync(userEntity);
            var userDto = _mapper.Map<UserResponseDTO>(userEntity);
            return ResponseModel<UserResponseDTO?>.Ok(userDto, "Usuário ativado com sucesso.");
        }
        catch (Exception ex)
        {
            return ResponseModel<UserResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}
