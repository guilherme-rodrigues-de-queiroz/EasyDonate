using EasyDonate.Domain.Enums;

namespace EasyDonate.Application.DTOs.Responses;

public class AppointmentResponseDTO
{
    public EAppointmentStatus? AppointmentStatus { get; set; }
    public DateTimeOffset? CollectionDate { get; set; }
}
