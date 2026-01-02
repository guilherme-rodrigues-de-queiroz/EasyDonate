using AutoMapper;
using EasyDonate.Application.DTOs.Requests.Donation;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces;
using EasyDonate.Application.Interfaces.Addresses;
using EasyDonate.Application.Interfaces.Appointments;
using EasyDonate.Application.Interfaces.Donations;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Services;

public class DonationService : IDonationService
{
    private readonly IMapper _mapper;
    private readonly IDonationRepository _donationRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IUnityOfWork _unityOfWork;

    public DonationService(IMapper mapper, IDonationRepository donationRepository, IAppointmentRepository appointmentRepository, IAddressRepository addressRepository, IUnityOfWork unityOfWork)
    {
        _mapper = mapper;
        _donationRepository = donationRepository;
        _appointmentRepository = appointmentRepository;
        _addressRepository = addressRepository;
        _unityOfWork = unityOfWork;
    }

    public async Task<ResponseModel<IReadOnlyList<DonationResponseDTO>?>> GetAllDonationsAsync()
    {
        try
        {
            var donationEntities = await _donationRepository.GetAllDonationsAsync();
            if (!donationEntities.Any()) return ResponseModel<IReadOnlyList<DonationResponseDTO>?>.NoContent();

            var donationDtos = _mapper.Map<IReadOnlyList<DonationResponseDTO>>(donationEntities);
            return ResponseModel<IReadOnlyList<DonationResponseDTO>?>.Ok(donationDtos, $"{donationEntities.Count} Doações encontradas com sucesso.");
        }
        catch (Exception ex)
        {
            return ResponseModel<IReadOnlyList<DonationResponseDTO>?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<IReadOnlyList<DonationResponseDTO>?>> GetDonationByDonorIdAsync(int id)
    {
        try
        {
            var donationEntity = await _donationRepository.GetDonationByDonorIdAsync(id);
            if (!donationEntity.Any()) return ResponseModel<IReadOnlyList<DonationResponseDTO>?>.NotFound($"Nenhuma doação encontrada.");

            var donationDto = _mapper.Map<IReadOnlyList<DonationResponseDTO>>(donationEntity);
            return ResponseModel<IReadOnlyList<DonationResponseDTO>?>.Ok(donationDto, "Doação encontrada com sucesso.");
        }
        catch (Exception ex)
        {
            return ResponseModel<IReadOnlyList<DonationResponseDTO>?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<IReadOnlyList<DonationResponseDTO>?>> GetDonationByOngIdAsync(int id)
    {
        try
        {
            var donationEntity = await _donationRepository.GetDonationByOngIdAsync(id);
            if (!donationEntity.Any()) return ResponseModel<IReadOnlyList<DonationResponseDTO>>.NotFound($"Nenhuma doação encontrada.");

            var donationDto = _mapper.Map<IReadOnlyList<DonationResponseDTO>>(donationEntity);
            return ResponseModel<IReadOnlyList<DonationResponseDTO>?>.Ok(donationDto, "Doação encontrada com sucesso.");
        }
        catch (Exception ex)
        {
            return ResponseModel<IReadOnlyList<DonationResponseDTO>?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<DonationResponseDTO?>> CreateDonationAsync(CreateDonationDTO dto)
    {
        await _unityOfWork.BeginTransactionAsync();

        try
        {
            var verifyDonorAddress = await _addressRepository.GetDonorAddressByIdAsync(dto.DonorId);

            if (verifyDonorAddress is null && dto.DonationType != Domain.Enums.EDonationType.money)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<DonationResponseDTO?>.UnprocessableEntity("Você não pode realizar uma doação, primeiro cadastre seu endereço.");
            }

            var donationEntity = _mapper.Map<Donation>(dto);

            if (dto.Description != null)
                donationEntity.Description = dto.Description.ToUpper();

            if (dto.ItemCondition != null)
                donationEntity.ItemCondition = dto.ItemCondition.ToUpper();

            await _donationRepository.CreateDonationAsync(donationEntity);

            if (donationEntity.Id <= 0)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<DonationResponseDTO?>.BadRequest("Não foi possível cadastrar uma doação.");
            }

            var appointmentEntity = _mapper.Map<Appointment>(dto);
            appointmentEntity.DonationId = donationEntity.Id;
            appointmentEntity.AppointmentStatus = Domain.Enums.EAppointmentStatus.Pending;

            await _appointmentRepository.CreateAppointmentAsync(appointmentEntity);

            if (appointmentEntity.Id <= 0)
            {
                await _unityOfWork.RollbackAsync();
                return ResponseModel<DonationResponseDTO?>.BadRequest("Não foi possível cadastrar o agendamento.");
            }

            await _unityOfWork.CommitAsync();

            var donationDto = _mapper.Map<DonationResponseDTO>(donationEntity);

            return ResponseModel<DonationResponseDTO?>.Created(donationDto, "Doação cadastrada com sucesso.");
        }
        catch (Exception ex)
        {
            await _unityOfWork.RollbackAsync();
            return ResponseModel<DonationResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}
