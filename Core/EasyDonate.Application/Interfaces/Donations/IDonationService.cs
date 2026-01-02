using EasyDonate.Application.DTOs.Requests.Donation;
using EasyDonate.Application.DTOs.Responses;

namespace EasyDonate.Application.Interfaces.Donations;

public interface IDonationService
{
    public Task<ResponseModel<IReadOnlyList<DonationResponseDTO>?>> GetAllDonationsAsync();
    public Task<ResponseModel<IReadOnlyList<DonationResponseDTO>?>> GetDonationByOngIdAsync(int id);
    public Task<ResponseModel<IReadOnlyList<DonationResponseDTO>?>> GetDonationByDonorIdAsync(int id);
    public Task<ResponseModel<DonationResponseDTO?>> CreateDonationAsync(CreateDonationDTO dto);
}
