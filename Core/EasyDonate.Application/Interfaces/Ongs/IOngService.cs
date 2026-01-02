using EasyDonate.Application.DTOs.Requests.Ongs;
using EasyDonate.Application.DTOs.Responses;

namespace EasyDonate.Application.Interfaces.Ongs;

public interface IOngService
{
    public Task<ResponseModel<IReadOnlyList<OngResponseDTO>?>> GetAllOngsAsync();
    public Task<ResponseModel<OngResponseDTO?>> GetOngByIdAsync(int id);
    public Task<ResponseModel<OngResponseDTO?>> CreateOngAsync(CreateOngDTO dto);
    public Task<ResponseModel<OngResponseDTO?>> UpdateOngAsync(int id, UpdateOngDTO dto);
}