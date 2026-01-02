using System.ComponentModel.DataAnnotations;

namespace EasyDonate.Application.DTOs.Requests.Donors;

public class UpdateDonorDTO
{
    [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
    [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:[ '][A-Za-zÀ-ÖØ-öø-ÿ]+)+$", ErrorMessage = "Informe o nome completo.")]
    public string? Name { get; set; }

    [MinLength(3, ErrorMessage = "O nome social deve ter pelo menos 3 caracteres.")]
    [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:[ '][A-Za-zÀ-ÖØ-öø-ÿ]+)*$", ErrorMessage = "Nome social inválido.")]
    public string? SocialName { get; set; }

    [RegularExpression(@"^\d{2}$", ErrorMessage = "DDD inválido.")]
    public string? Ddd { get; set; }

    [RegularExpression(@"^\d{8,9}$", ErrorMessage = "Telefone inválido.")]
    public string? Phone { get; set; }
}