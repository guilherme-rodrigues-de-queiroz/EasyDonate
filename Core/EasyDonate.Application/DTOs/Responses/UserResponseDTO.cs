namespace EasyDonate.Application.DTOs.Responses;

public class UserResponseDTO
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public bool Active {  get; set; }
}
