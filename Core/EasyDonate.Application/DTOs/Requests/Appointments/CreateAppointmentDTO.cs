using EasyDonate.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyDonate.Application.DTOs.Requests.Appointments;

public class CreateAppointmentDTO
{
    [Required(ErrorMessage = "Informe o status do agendamento.")]
    public EAppointmentStatus AppointmentStatus { get; init; }

    public DateTimeOffset? CollectionDate { get; init; }

    [Required]
    public int DonationId { get; init; }
}