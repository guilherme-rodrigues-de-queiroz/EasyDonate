using EasyDonate.Application.Interfaces.Ongs;
using EasyDonate.Domain.Entities;
using EasyDonate.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyDonate.Persistence.Repositories;

public class OngRepository : IOngRepository
{
    private readonly AppDbContext _context;

    public OngRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Ong>> GetAllOngsAsync()
    {
        return await _context.Ongs.ToListAsync();
    }

    public async Task<Ong?> GetOngByIdAsync(int id)
    {
        return await _context.Ongs
            .Include(o => o.Address)
            .Include(o => o.Donations)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Ong?> GetOngByCnpjAsync(string cnpj)
    {
        return await _context.Ongs.FirstOrDefaultAsync(o => o.Cnpj == cnpj);
    }

    public async Task<Ong> CreateOngAsync(Ong ong)
    {
        _context.Ongs.Add(ong);
        await _context.SaveChangesAsync();
        return ong;
    }

    public async Task<Ong> UpdateOngAsync(Ong ong)
    {
        _context.Ongs.Update(ong);
        await _context.SaveChangesAsync();
        return ong;
    }

    public async Task<Ong> DeleteOngAsync(Ong ong)
    {
        _context.Ongs.Remove(ong);
        await _context.SaveChangesAsync();
        return ong;
    }
}