using MedEase.Core.Dtos;

using MedEase.Core.Dtos;
using MedEase.Core.Models;

namespace MedEase.EF.Services
{
    public interface IDoctorService
    {
        Task<List<DoctorAppointmentAndPatternDto>> GetPatternAndAppointmentAsync(int Id);
        Task<Appointment> ReserveAppointmentAsync(ReserveAppointmentDto appointmentDto);
        Task<DoctorSchedule> CreateScheduleAsync(DoctorScheduleDto ScheduleDto);
        Task<List<DoctorInfoGetDto>> GetAll();
        Task<IEnumerable<ReviewDto>> GetDoctorReviews(int Id);
        Task<ReviewDto> CreateReview(ReviewDto dto);
    }
}