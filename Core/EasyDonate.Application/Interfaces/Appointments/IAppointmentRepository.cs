using EasyDonate.Domain.Entities;

namespace EasyDonate.Application.Interfaces.Appointments;

public interface IAppointmentRepository
{
    Task<Appointment> CreateAppointmentAsync(Appointment appointment);
}
