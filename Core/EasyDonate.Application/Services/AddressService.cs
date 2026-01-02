using System.Net;
using AutoMapper;
using EasyDonate.Application.DTOs.Requests.Addresses;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces.Addresses;
using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Services;

public class AddressService : IAddressService
{
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;

    public AddressService(IMapper mapper, IAddressRepository addressRepository)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    public async Task<ResponseModel<AddressResponseDTO?>> GetOngAddressByIdAsync(int id)
    {
        try
        {
            var addressEntity = await _addressRepository.GetOngAddressByIdAsync(id);
            if (addressEntity is null) return ResponseModel<AddressResponseDTO>.NotFound($"Nenhum endereço encontrado com a ONG informada.");

            var addressDto = _mapper.Map<AddressResponseDTO>(addressEntity);
            return ResponseModel<AddressResponseDTO?>.Ok(addressDto, "Endereço encontrado com sucesso.");
        }
        catch (Exception ex)
        {
            return ResponseModel<AddressResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<AddressResponseDTO?>> GetDonorAddressByIdAsync(int id)
    {
        try
        {
            var addressEntity = await _addressRepository.GetDonorAddressByIdAsync(id);
            if (addressEntity is null) return ResponseModel<AddressResponseDTO>.NotFound($"Nenhum endereço encontrado com o doador informado.");

            var addressDto = _mapper.Map<AddressResponseDTO>(addressEntity);
            return ResponseModel<AddressResponseDTO?>.Ok(addressDto, "Endereço encontrado com sucesso.");
        }
        catch (Exception ex)
        {
            return ResponseModel<AddressResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<ResponseModel<AddressResponseDTO?>> CreateAddressAsync(CreateAddressDTO dto)
    {
        if ((dto.OngId is null && dto.DonorId is null) || (dto.OngId is not null && dto.DonorId is not null))
            return ResponseModel<AddressResponseDTO?>.BadRequest("Informe apenas o ID da ONG ou apenas o ID do doador.");

        try
        {
            if (dto.DonorId.HasValue)
            {
                var donorHasAddress = await _addressRepository.DonorHasAddressAsync(dto.DonorId.Value);
                if (donorHasAddress) return ResponseModel<AddressResponseDTO>.Conflict("Já existe um endereço cadastrado para esse doador.");
            }
            
            if (dto.OngId.HasValue)
            {
                var ongHasAddress = await _addressRepository.OngHasAddressAsync(dto.OngId.Value);
                if (ongHasAddress) return ResponseModel<AddressResponseDTO>.Conflict("Já existe um endereço cadastrado para essa ONG.");
            }

            var addressEntity = _mapper.Map<Address>(dto);

            addressEntity.Cep = dto.Cep.ToUpper();
            addressEntity.StreetAddress = dto.StreetAddress.ToUpper();
            addressEntity.Number = dto.Number.ToUpper();
            addressEntity.Neighborhood = dto.Neighborhood.ToUpper();
            addressEntity.City = dto.City.ToUpper();
            addressEntity.State = dto.State.ToUpper();

            if (dto.Complement != null)
                addressEntity.Complement = dto.Complement.ToUpper();

            if (addressEntity.OngId.HasValue)
            {
                var googleLocation = $"{addressEntity.StreetAddress}, {addressEntity.Number}, {addressEntity.Cep}";
                var encodedGoogleLocation = WebUtility.UrlEncode(googleLocation);

                addressEntity.GoogleLocation = encodedGoogleLocation;
            }

            await _addressRepository.CreateAddressAsync(addressEntity);

            var addressDto = _mapper.Map<AddressResponseDTO>(addressEntity);
            return ResponseModel<AddressResponseDTO?>.Created(addressDto, "Endereço atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return ResponseModel<AddressResponseDTO?>.Fail($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}
