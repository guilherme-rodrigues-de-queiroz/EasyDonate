using EasyDonate.Application.Interfaces.Donations;
using EasyDonate.Domain.Entities;
using EasyDonate.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyDonate.Persistence.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly AppDbContext _context;

    public DonationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Donation>> GetAllDonationsAsync()
    {
        return await _context.Donations.ToListAsync();
    }

    public async Task<IReadOnlyList<Donation>> GetDonationByDonorIdAsync(int id)
    {
        return await _context.Donations
            .Include(d => d.Appointment)
            .Where(d => d.DonorId == id)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Donation>> GetDonationByOngIdAsync(int id)
    {
        return await _context.Donations
            .Include(d => d.Appointment)
            .Where(d => d.OngId == id)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync();
    }

    public async Task<Donation> CreateDonationAsync(Donation donation)
    {
        _context.Donations.Add(donation);
        await _context.SaveChangesAsync();
        return donation;
    }
}
