using System.ComponentModel.DataAnnotations;
using EasyDonate.Application.Validations;

namespace EasyDonate.Application.DTOs.Requests.Ongs;

public class CreateOngDTO
{
    [Required(ErrorMessage = "Informe o nome da organização.")]
    [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
    [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:[ '][A-Za-zÀ-ÖØ-öø-ÿ]+)*$", ErrorMessage = "Nome inválido.")]
    public string Name { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe o CNPJ.")]
    [CpfCnpj(ErrorMessage = "CNPJ inválido.")]
    public string Cnpj { get; init; } = string.Empty;

    public string? ActivityType { get; init; }
    public string? Description { get; init; }

    [Required(ErrorMessage = "Informe o DDD.")]
    [RegularExpression(@"^\d{2}$", ErrorMessage = "DDD inválido.")]
    public string Ddd { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe o número de telefone.")]
    [RegularExpression(@"^\d{8,9}$", ErrorMessage = "Telefone inválido.")]
    public string Phone { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe o nome completo do responsável pelo cadastro da ONG.")]
    public string RegistrationManager { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe o email.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha.")]
    public string Password { get; init; } = string.Empty;

    [Required]
    [Compare(nameof(Password), ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmPassword { get; init; } = string.Empty;
}
