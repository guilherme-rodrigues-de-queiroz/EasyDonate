using EasyDonate.Domain.Enums;

namespace EasyDonate.Domain.Entities;

public sealed class Appointment : BaseEntity
{
    public EAppointmentStatus AppointmentStatus { get; set; }
    public DateTimeOffset? CollectionDate { get; set; }

    public int DonationId { get; set; }
    public Donation Donation { get; set; } = null!;
}