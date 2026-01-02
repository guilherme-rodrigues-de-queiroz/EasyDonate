using EasyDonate.Application.DTOs.Requests.Addresses;
using EasyDonate.Application.DTOs.Responses;

namespace EasyDonate.Application.Interfaces.Addresses;

public interface IAddressService
{
    public Task<ResponseModel<AddressResponseDTO?>> GetOngAddressByIdAsync(int id);
    public Task<ResponseModel<AddressResponseDTO?>> GetDonorAddressByIdAsync(int id);
    public Task<ResponseModel<AddressResponseDTO?>> CreateAddressAsync(CreateAddressDTO dto);
}
