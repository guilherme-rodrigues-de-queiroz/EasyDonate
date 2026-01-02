namespace EasyDonate.Application.DTOs.Responses;

public class AddressResponseDTO
{
    public string? Cep {  get; set; }
    public string? StreetAddress { get; set; }
    public string? Number { get; set; }
    public string? Complement {  get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? GoogleLocation { get; set; }
}
