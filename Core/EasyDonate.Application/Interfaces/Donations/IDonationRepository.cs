using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Interfaces.Donations;

public interface IDonationRepository
{
    Task<IReadOnlyList<Donation>> GetAllDonationsAsync();
    Task<IReadOnlyList<Donation>> GetDonationByOngIdAsync(int id);
    Task<IReadOnlyList<Donation>> GetDonationByDonorIdAsync(int id);
    Task<Donation> CreateDonationAsync(Donation donation);
}
