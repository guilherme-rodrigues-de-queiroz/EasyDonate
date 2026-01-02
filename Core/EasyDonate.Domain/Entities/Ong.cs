namespace EasyDonate.Domain.Entities;

public sealed class Ong : BaseEntity
{
    public string? Name { get; set; }
    public string? Cnpj { get; set; }
    public string? ActivityType { get; set; }
    public string? Description { get; set; }
    public string? Ddd { get; set; }
    public string? Phone { get; set; }
    public string? RegistrationManager { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public Address? Address { get; set; }

    public ICollection<Donation>? Donations { get; set; }
}
