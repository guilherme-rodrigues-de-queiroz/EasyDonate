using AutoMapper;
using EasyDonate.Application.DTOs.Requests.Donors;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces;
using EasyDonate.Application.Interfaces.Donors;
using EasyDonate.Application.Interfaces.Security;
using EasyDonate.Application.Interfaces.Users;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Services;

public class DonorService : IDonorService
{
    private readonly IMapper _mapper;
    private readonly IDonorRepository _donorRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public DonorService(IMapper mapper, IDonorRepository donorRepository, IUserRepository userRepository, IUnityOfWork unityOfWork, IPasswordHasher passwordHasher)
    {
        _mapper = mapper;
        _donorRepository = donorRepository;
        _userRepository = userRepository;
        _unityOfWork = unityOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<ResponseModel<IReadOnlyList<DonorResponseDTO>?>> GetAllDonorsAsync()
    {
        try
        {
            var donorEntities = await _donorRepository.GetAllDonorsAsync();
            if (donorEntities.Count == 0) return ResponseModel<IReadOnlyList<DonorResponseDTO>?>.NoContent();

            var donorDtos = _mapper.Map<IReadOnlyList<DonorResponseDTO>>(donorEntities);
            return ResponseModel<IReadOnlyList<DonorResponseDTO>?>.Ok(donorDtos, $"{donorEntities.Count} Doador(es) encontrado(s) com sucesso.");
        }
        catch (Exception ex)
        {
            return ResponseModel<IReadOnlyList<DonorResponseDTO>?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<DonorResponseDTO?>> GetDonorByIdAsync(int id)
    {
        try
        {
            var donorEntity = await _donorRepository.GetDonorByIdAsync(id);
            if (donorEntity is null) return ResponseModel<DonorResponseDTO>.NotFound($"Nenhum doador encontrado.");

            var donorDto = _mapper.Map<DonorResponseDTO>(donorEntity);
            return ResponseModel<DonorResponseDTO?>.Ok(donorDto, "Doador encontrado com sucesso.");
        } 
        catch (Exception ex)
        {
            return ResponseModel<DonorResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<DonorResponseDTO?>> CreateDonorAsync(CreateDonorDTO dto)
    {
        await _unityOfWork.BeginTransactionAsync();

        try
        {
            var userHasTheSameEmail = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (userHasTheSameEmail?.Email == dto.Email)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<DonorResponseDTO>.Conflict("Esse Email já está em uso.");
            }

            var donorHasTheSameCpfCnpj = await _donorRepository.GetDonorByCpfCnpjAsync(dto.CpfCnpj);

            if (donorHasTheSameCpfCnpj?.CpfCnpj == dto.CpfCnpj)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<DonorResponseDTO>.Conflict("Esse CPF/CNPJ já está em uso.");
            }

            var userEntity = _mapper.Map<User>(dto);
            userEntity.AssignAsDonor();
            userEntity.Email = dto.Email.ToLower();
            userEntity.Password = _passwordHasher.Hash(dto.Password);

            await _userRepository.CreateUserAsync(userEntity);

            if (userEntity.Id <= 0)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<DonorResponseDTO?>.BadRequest("Não foi possível criar um usuário.");
            }

            var donorEntity = _mapper.Map<Donor>(dto);
            donorEntity.UserId = userEntity.Id;
            donorEntity.Name = dto.Name.ToUpper();
            
            if (dto.SocialName != null)
                donorEntity.SocialName = dto.SocialName.ToUpper();

            await _donorRepository.CreateDonorAsync(donorEntity);

            if (donorEntity.Id <= 0)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<DonorResponseDTO?>.BadRequest("Não foi possível criar um doador.");
            }

            await _unityOfWork.CommitAsync();

            var donorDto = _mapper.Map<DonorResponseDTO>(donorEntity);

            return ResponseModel<DonorResponseDTO?>.Created(donorDto, "Doador cadastrado com sucesso.");
        } 
        catch (Exception ex)
        {
            await _unityOfWork.RollbackAsync();
            return ResponseModel<DonorResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<DonorResponseDTO?>> UpdateDonorAsync(int id, UpdateDonorDTO dto)
    {
        try
        {
            var donorEntity = await _donorRepository.GetDonorByIdAsync(id);
            if (donorEntity is null) return ResponseModel<DonorResponseDTO>.NotFound("Nenhum doador encontrado.");

            _mapper.Map(dto, donorEntity);
            donorEntity.UpdatedAt = DateTime.UtcNow;

            if (dto.Name != null)
                donorEntity.Name = dto.Name.ToUpper();

            if (dto.SocialName != null)
                donorEntity.SocialName = dto.SocialName.ToUpper();

            await _donorRepository.UpdateDonorAsync(donorEntity);
            var donorDto = _mapper.Map<DonorResponseDTO>(donorEntity);
            return ResponseModel<DonorResponseDTO?>.Ok(donorDto, "Doador atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return ResponseModel<DonorResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}
