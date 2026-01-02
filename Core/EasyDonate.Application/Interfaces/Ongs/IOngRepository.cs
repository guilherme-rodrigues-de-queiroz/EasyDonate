using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Interfaces.Ongs;

public interface IOngRepository
{
    Task<IReadOnlyList<Ong>> GetAllOngsAsync();
    Task<Ong?> GetOngByIdAsync(int id);
    Task<Ong?> GetOngByCnpjAsync(string cnpj);
    Task<Ong> CreateOngAsync(Ong ong);
    Task<Ong> UpdateOngAsync(Ong ong);
}