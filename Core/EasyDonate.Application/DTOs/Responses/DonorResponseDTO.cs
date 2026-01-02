namespace EasyDonate.Application.DTOs.Responses;

public class DonorResponseDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? SocialName { get; set; }
    public string? CpfCnpj { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Ddd { get; set; }
    public string? Phone { get; set; }
    public AddressResponseDTO? Address { get; set; }
    public IReadOnlyList<DonationResponseDTO>? Donations { get; set; }
}
