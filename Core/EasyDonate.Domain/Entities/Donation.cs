using EasyDonate.Domain.Enums;

namespace EasyDonate.Domain.Entities;

public sealed class Donation : BaseEntity
{
    public EDonationType DonationType { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? ItemCondition { get; set; }
    public DateTime? FoodExpirationDate { get; set; }

    public int DonorId { get; set; }
    public Donor Donor { get; set; } = null!;

    public int OngId { get; set; }
    public Ong Ong { get; set; } = null!;

    public Appointment Appointment { get; set; } = null!;
}
