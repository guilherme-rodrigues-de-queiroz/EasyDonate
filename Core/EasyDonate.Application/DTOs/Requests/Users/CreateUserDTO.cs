using System.ComponentModel.DataAnnotations;

namespace EasyDonate.Application.DTOs.Requests.Users;

public class CreateUserDTO
{
    [Required(ErrorMessage = "Informe o email.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha.")]
    public string Password { get; init; } = string.Empty;

    [Required]
    [Compare(nameof(Password), ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmPassword { get; init; } = string.Empty;
}
