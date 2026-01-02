using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Interfaces.Addresses;

public interface IAddressRepository
{
    Task<Address?> GetOngAddressByIdAsync(int id);
    Task<Address?> GetDonorAddressByIdAsync(int id);
    Task<Address> CreateAddressAsync(Address address);
    Task<bool> DonorHasAddressAsync(int donorId);
    Task<bool> OngHasAddressAsync(int ongId);
}
