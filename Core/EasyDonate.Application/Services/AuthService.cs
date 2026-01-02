using AutoMapper;
using EasyDonate.Application.DTOs.Requests.Auth;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces.Auth;
using EasyDonate.Application.Interfaces.Security;
using EasyDonate.Application.Interfaces.Users;

namespace EasyDonate.Application.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public AuthService(ITokenService tokenService, IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<ResponseModel<TokenResponseDTO?>> LoginAsync(LoginDto dto)
    {
        try
        {
            var userEntity = await _userRepository.GetUserByEmailAsync(dto.Email.ToLower());

            if (userEntity == null)
                return ResponseModel<TokenResponseDTO?>.BadRequest("Email ou senha inválidos.");

            var isPasswordValid = _passwordHasher.Verify(dto.Password, userEntity.Password);

            if (!isPasswordValid)
                return ResponseModel<TokenResponseDTO?>.BadRequest("Email ou senha inválidos.");

            var token = _tokenService.GenerateToken(userEntity);

            var response = _mapper.Map<TokenResponseDTO>(userEntity);
            response.AccessToken = token.AccessToken;
            response.ExpiresIn = token.ExpiresIn;

            return ResponseModel<TokenResponseDTO?>.Ok(response, "Login realizado com sucesso!");
        }
        catch (Exception ex)
        {
            return ResponseModel<TokenResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}
