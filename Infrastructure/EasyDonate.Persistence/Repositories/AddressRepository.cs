using EasyDonate.Application.Interfaces.Addresses;
using EasyDonate.Domain.Entities;
using EasyDonate.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyDonate.Persistence.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Address?> GetOngAddressByIdAsync(int id)
    {
        return await _context.Addresses.FirstOrDefaultAsync(a => a.OngId == id);
    }

    public async Task<Address?> GetDonorAddressByIdAsync(int id)
    {
        return await _context.Addresses.FirstOrDefaultAsync(a => a.DonorId == id);
    }

    public async Task<Address> CreateAddressAsync(Address address)
    {
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<bool> DonorHasAddressAsync(int donorId)
    {
        return await _context.Addresses.AnyAsync(a => a.DonorId == donorId);
    }

    public async Task<bool> OngHasAddressAsync(int ongId)
    {
        return await _context.Addresses.AnyAsync(a => a.OngId == ongId);
    }
}
