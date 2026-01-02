using AutoMapper;
using EasyDonate.Application.DTOs.Requests.Ongs;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces;
using EasyDonate.Application.Interfaces.Ongs;
using EasyDonate.Application.Interfaces.Security;
using EasyDonate.Application.Interfaces.Users;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Services;

public class OngService : IOngService
{
    private readonly IMapper _mapper;
    private readonly IOngRepository _ongRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public OngService(IMapper mapper, IOngRepository ongRepository, IUserRepository userRepository, IUnityOfWork unityOfWork, IPasswordHasher passwordHasher)
    {
        _mapper = mapper;
        _ongRepository = ongRepository;
        _userRepository = userRepository;
        _unityOfWork = unityOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<ResponseModel<IReadOnlyList<OngResponseDTO>?>> GetAllOngsAsync()
    {
        try
        {
            var ongEntities = await _ongRepository.GetAllOngsAsync();
            if (ongEntities.Count == 0) return ResponseModel<IReadOnlyList<OngResponseDTO>?>.NoContent();

            var ongDtos = _mapper.Map<IReadOnlyList<OngResponseDTO>>(ongEntities);
            return ResponseModel<IReadOnlyList<OngResponseDTO>?>.Ok(ongDtos, $"{ongEntities.Count} ONG(s) encontrada(s) com sucesso.");
        } 
        catch(Exception ex)
        {
            return ResponseModel<IReadOnlyList<OngResponseDTO>?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<OngResponseDTO?>> GetOngByIdAsync(int id)
    {
        try
        {
            var ongEntity = await _ongRepository.GetOngByIdAsync(id);
            if (ongEntity is null) return ResponseModel<OngResponseDTO>.NotFound($"Nenhuma ONG encontrada.");

            var ongDto = _mapper.Map<OngResponseDTO>(ongEntity);
            return ResponseModel<OngResponseDTO?>.Ok(ongDto, "ONG encontrada com sucesso.");
        } catch (Exception ex)
        {
            return ResponseModel<OngResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<OngResponseDTO?>> CreateOngAsync(CreateOngDTO dto)
    {
        await _unityOfWork.BeginTransactionAsync();

        try
        {
            var userHasTheSameEmail = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (userHasTheSameEmail?.Email == dto.Email)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<OngResponseDTO>.Conflict("Esse Email já está em uso.");
            }

            var ongHasTheSameCnpj = await _ongRepository.GetOngByCnpjAsync(dto.Cnpj);

            if (ongHasTheSameCnpj?.Cnpj == dto.Cnpj)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<OngResponseDTO>.Conflict("Esse CNPJ já está em uso.");
            }

            var userEntity = _mapper.Map<User>(dto);
            userEntity.AssignAsOng();
            userEntity.Email = dto.Email.ToLower();
            userEntity.Password = _passwordHasher.Hash(dto.Password);

            await _userRepository.CreateUserAsync(userEntity);

            if (userEntity.Id <= 0)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<OngResponseDTO?>.BadRequest("Não foi possível criar um usuário.");
            }

            var ongEntity = _mapper.Map<Ong>(dto);
            ongEntity.UserId = userEntity.Id;
            ongEntity.Name = dto.Name.ToUpper();
            ongEntity.RegistrationManager = dto.RegistrationManager.ToUpper();

            await _ongRepository.CreateOngAsync(ongEntity);

            if (ongEntity.Id <= 0)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<OngResponseDTO?>.BadRequest("Não foi possível criar uma ONG.");
            }

            await _unityOfWork.CommitAsync();

            var ongDto = _mapper.Map<OngResponseDTO>(ongEntity);

            return ResponseModel<OngResponseDTO?>.Created(ongDto, "ONG cadastrada com sucesso.");
        }
        catch (Exception ex)
        {
            await _unityOfWork.RollbackAsync();
            return ResponseModel<OngResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<OngResponseDTO?>> UpdateOngAsync(int id, UpdateOngDTO dto)
    {
        try
        {
            var ongEntity = await _ongRepository.GetOngByIdAsync(id);
            if (ongEntity is null) return ResponseModel<OngResponseDTO>.NotFound("Nenhuma ONG encontrada.");

            _mapper.Map(dto, ongEntity);
            ongEntity.UpdatedAt = DateTime.UtcNow;

            if (dto.Name != null)
                ongEntity.Name = dto.Name.ToUpper();

            if (dto.ActivityType != null)
                ongEntity.ActivityType = dto.ActivityType.ToUpper();

            if (dto.Description != null)
                ongEntity.Description = dto.Description.ToUpper();

            await _ongRepository.UpdateOngAsync(ongEntity);
            var ongDto = _mapper.Map<OngResponseDTO>(ongEntity);
            return ResponseModel<OngResponseDTO?>.Ok(ongDto, "ONG atualizada com sucesso.");
        } 
        catch (Exception ex)
        {
            return ResponseModel<OngResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}
