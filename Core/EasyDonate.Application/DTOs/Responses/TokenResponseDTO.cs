using EasyDonate.Domain.Enums;

namespace EasyDonate.Application.DTOs.Responses;

public class TokenResponseDTO
{
    public int Id { get; set; }
    public string AccessToken { get; set; } = null!;
    public string TokenType {  get; set; } = "Bearer";
    public int ExpiresIn { get; set; }
    public EUserType UserType { get; set; }
    public bool Active { get; set; }
    public string? Email { get; set; }
}
