using EasyDonate.Application.Validations;
using EasyDonate.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyDonate.Application.DTOs.Requests.Donors;

public class CreateDonorDTO
{
    [Required(ErrorMessage = "Informe o tipo de doador.")]
    public EDonorType DonorType { get; init; }

    [Required(ErrorMessage = "Informe o nome completo.")]
    [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
    [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:[ '][A-Za-zÀ-ÖØ-öø-ÿ]+)+$", ErrorMessage = "Informe o nome completo.")]
    public string Name { get; init; } = string.Empty;

    [MinLength(3, ErrorMessage = "O nome social deve ter pelo menos 3 caracteres.")]
    [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:[ '][A-Za-zÀ-ÖØ-öø-ÿ]+)*$", ErrorMessage = "Nome social inválido.")]
    public string? SocialName { get; init; }

    [Required(ErrorMessage = "Informe o documento de identificação.")]
    [CpfCnpj(ErrorMessage = "Documento inválido.")]
    public string CpfCnpj { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe a data de nascimento.")]
    public DateTime BirthDate { get; init; }

    [Required(ErrorMessage = "Informe o DDD.")]
    [RegularExpression(@"^\d{2}$", ErrorMessage = "DDD inválido.")]
    public string Ddd { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe o número de telefone.")]
    [RegularExpression(@"^\d{8,9}$", ErrorMessage = "Telefone inválido.")]
    public string Phone { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe o email")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha.")]
    public string Password { get; init; } = string.Empty;

    [Required]
    [Compare(nameof(Password), ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmPassword { get; init; } = string.Empty;
}