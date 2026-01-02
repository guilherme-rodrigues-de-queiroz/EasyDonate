using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Interfaces.Donors;

public interface IDonorRepository
{
    Task<IReadOnlyList<Donor>> GetAllDonorsAsync();
    Task<Donor?> GetDonorByIdAsync(int id);
    Task<Donor?> GetDonorByCpfCnpjAsync(string cpfCnpj);
    Task<Donor> CreateDonorAsync(Donor donor);
    Task<Donor> UpdateDonorAsync(Donor donor);
}
