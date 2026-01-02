using System.ComponentModel.DataAnnotations;
using EasyDonate.Application.Validations;

namespace EasyDonate.Application.DTOs.Requests.Ongs;

public class UpdateOngDTO
{
    [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
    [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:[ '][A-Za-zÀ-ÖØ-öø-ÿ]+)*$", ErrorMessage = "Nome inválido.")]
    public string? Name { get; set; }

    [CpfCnpj(ErrorMessage = "Documento inválido.")]
    public string? Cnpj { get; set; }

    public string? ActivityType { get; set; }

    public string? Description { get; set; }

    [RegularExpression(@"^\d{2}$", ErrorMessage = "DDD inválido.")]
    public string? Ddd { get; set; }

    [RegularExpression(@"^\d{8,9}$", ErrorMessage = "Telefone inválido.")]
    public string? Phone { get; set; }
}
