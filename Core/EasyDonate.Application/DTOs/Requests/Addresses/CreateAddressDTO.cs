using System.ComponentModel.DataAnnotations;

namespace EasyDonate.Application.DTOs.Requests.Addresses;

public class CreateAddressDTO
{
    [Required(ErrorMessage = "Informe o CEP.")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP inválido.")]
    public string Cep { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe o logradouro.")]
    public string StreetAddress { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe o número.")]
    public string Number { get; init; } = string.Empty;

    public string? Complement { get; init; }

    [Required(ErrorMessage = "Informe o bairro.")]
    public string Neighborhood { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe a cidade.")]
    public string City { get; init; } = string.Empty;

    [Required(ErrorMessage = "Informe o estado.")]
    [RegularExpression(@"^(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)$", ErrorMessage = "Estado inválido.")]
    public string State { get; init; } = string.Empty;

    public int? DonorId { get; init; }
    public int? OngId { get; init; }
}
