using System.ComponentModel.DataAnnotations;

namespace EasyDonate.Application.DTOs.Requests.Addresses;

public class UpdateAddressDTO
{
    [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP inválido.")]
    public string? Cep { get; set; }
    public string? StreetAddress { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    [RegularExpression(@"^(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)$", ErrorMessage = "Estado inválido.")]
    public string? State { get; set; }
    public int? DonorId { get; set; }
    public int? OngId { get; set; }
}
