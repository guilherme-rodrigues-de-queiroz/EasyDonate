using EasyDonate.Domain.Enums;

namespace EasyDonate.Application.DTOs.Responses;

public class DonationResponseDTO
{
    public EDonationType? DonationType { get; set; }
    public decimal? Amount { get; set; }
    public string? Description { get; set; }
    public string? ItemCondition { get; set; }
    public DateOnly? FoodExpirationDate { get; set; }
    public AppointmentResponseDTO? Appointment { get; set; }
}
