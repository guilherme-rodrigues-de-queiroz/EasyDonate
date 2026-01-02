using EasyDonate.Domain.Enums;

namespace EasyDonate.Domain.Entities;

public sealed class Donor : BaseEntity
{
    public EDonorType DonorType { get; set; }
    public string? Name { get; set; }
    public string? SocialName { get; set; }
    public string? CpfCnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Ddd { get; set; }
    public string? Phone { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public Address? Address { get; set; }
    public ICollection<Donation>? Donations { get; set; }
}