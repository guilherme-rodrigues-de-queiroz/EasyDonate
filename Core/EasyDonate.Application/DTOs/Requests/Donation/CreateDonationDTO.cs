using EasyDonate.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyDonate.Application.DTOs.Requests.Donation;

public class CreateDonationDTO
{
    [Required(ErrorMessage = "Informe o tipo de doação.")]
    public EDonationType DonationType { get; init; }

    [Required(ErrorMessage = "Informe a quantidade")]
    public decimal Amount { get; init; }

    public string? Description { get; init; }

    public string? ItemCondition { get; init; }

    public DateTime? FoodExpirationDate {  get; init; }

    public EAppointmentStatus AppointmentStatus { get; init; }

    public DateTimeOffset? CollectionDate { get; init; }

    [Required]
    public int DonorId { get; init; }

    [Required]
    public int OngId { get; init; }
}