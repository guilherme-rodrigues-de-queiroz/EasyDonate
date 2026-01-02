using EasyDonate.Application.Interfaces.Appointments;
using EasyDonate.Domain.Entities;
using EasyDonate.Persistence.Context;

namespace EasyDonate.Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }
}
