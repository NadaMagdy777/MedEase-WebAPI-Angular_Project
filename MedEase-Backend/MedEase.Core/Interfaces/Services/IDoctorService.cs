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
        public Task<Appointment> ReserveAppointmentAsync(ReserveAppointmentDto appointmentDto);
        public Task<DoctorSchedule> CreateScheduleAsync(DoctorScheduleDto ScheduleDto);
        public  Task<List<DoctorInfoGetDto>> GetAll();
        public Task<bool> EditDoctor(DoctorEditDto doctorDto, int id);
        public  Task<bool> AddDoctorSubspiciality(int DoctorID, SubspecialityDto subspeciality);

    }
}