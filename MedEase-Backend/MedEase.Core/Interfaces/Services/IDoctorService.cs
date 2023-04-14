using MedEase.Core.Dtos;
using MedEase.Core.Models;

namespace MedEase.EF.Services
{
    public interface IDoctorService
    {
        public Task<Appointment> ReserveAppointmentAsync(ReserveAppointmentDto appointmentDto);
        public Task<DoctorSchedule> CreateScheduleAsync(DoctorScheduleDto ScheduleDto);
    }
}