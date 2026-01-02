using EasyDonate.Application.DTOs.Requests.Donors;
using EasyDonate.Application.DTOs.Responses;

namespace EasyDonate.Application.Interfaces.Donors;

public interface IDonorService
{
    public Task<ResponseModel<IReadOnlyList<DonorResponseDTO>?>> GetAllDonorsAsync();
    public Task<ResponseModel<DonorResponseDTO?>> GetDonorByIdAsync(int id);
    public Task<ResponseModel<DonorResponseDTO?>> CreateDonorAsync(CreateDonorDTO dto);
    public Task<ResponseModel<DonorResponseDTO?>> UpdateDonorAsync(int id, UpdateDonorDTO dto);
}
