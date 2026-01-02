using EasyDonate.Application.Interfaces.Donors;
using EasyDonate.Domain.Entities;
using EasyDonate.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyDonate.Persistence.Repositories;

public class DonorRepository : IDonorRepository
{
    private readonly AppDbContext _context;

    public DonorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Donor>> GetAllDonorsAsync()
    {
        return await _context.Donors.ToListAsync();
    }

    public async Task<Donor?> GetDonorByIdAsync(int id)
    {
        return await _context.Donors
            .Include(d => d.Address)
            .Include(d => d.Donations)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Donor?> GetDonorByCpfCnpjAsync(string cpfCnpj)
    {
        return await _context.Donors.FirstOrDefaultAsync(d => d.CpfCnpj == cpfCnpj);
    }

    public async Task<Donor> CreateDonorAsync(Donor donor)
    {
        _context.Donors.Add(donor);
        await _context.SaveChangesAsync();
        return donor;
    }

    public async Task<Donor> UpdateDonorAsync(Donor donor)
    {
        _context.Donors.Update(donor);
        await _context.SaveChangesAsync();
        return donor;
    }

    public async Task<Donor> DeleteDonorAsync(Donor donor)
    {
        _context.Donors.Remove(donor);
        await _context.SaveChangesAsync();
        return donor;
    }
}
