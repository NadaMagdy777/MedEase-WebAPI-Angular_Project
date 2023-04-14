using MedEase.Core.Dtos;

using MedEase.Core.Dtos;
using MedEase.Core.Models;

namespace MedEase.EF.Services
{
    public interface IDoctorService
    {
        Task<List<DoctorAppointmentAndPatternDto>> GetPatternAndAppointmentAsync(int Id);
        public Task<Appointment> ReserveAppointmentAsync(ReserveAppointmentDto appointmentDto);
        public Task<DoctorSchedule> CreateScheduleAsync(DoctorScheduleDto ScheduleDto);
        public  Task<List<DoctorInfoGetDto>> GetAll();
    }
}