using System.ComponentModel.DataAnnotations;

namespace EasyDonate.Application.DTOs.Requests.Auth;

public class LoginDto
{
    [Required(ErrorMessage = "Email ou senha inválidos.")]
    public string Email { get; init; } = string.Empty;

    [Required(ErrorMessage = "Email ou senha inválidos.")]
    public string Password { get; init; } = string.Empty;
}