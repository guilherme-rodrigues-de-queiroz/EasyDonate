namespace EasyDonate.Application.DTOs.Responses;

public class TokenResultDTO
{
    public string AccessToken { get; set; } = null!;
    public int ExpiresIn { get; set; }
}
