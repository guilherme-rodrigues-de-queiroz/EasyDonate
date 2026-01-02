namespace EasyDonate.Application.DTOs.Responses;

public class OngResponseDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Cnpj { get; set; }
    public string? ActivityType { get; set; }
    public string? Description { get; set; }
    public string? Ddd { get; set; }
    public string? Phone { get; set; }
    public string? RegistrationManager { get; set; }
    public AddressResponseDTO? Address { get; set; }
    public IReadOnlyList<DonationResponseDTO>? Donations { get; set; }
}
