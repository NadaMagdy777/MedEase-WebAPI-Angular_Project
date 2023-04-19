using MedEase.Core;
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
     
        Task<bool> EditDoctor(DoctorEditDto doctorDto, int id);
        Task<bool> AddDoctorSubspiciality(int DoctorID, SubspecialityDto subspeciality);
        Task<bool> AddDoctorCertificate(int DoctorID, CertificateDto certificate);
        Task<bool> AddDoctorInsurance(int DoctorID, InsuranceDto InsuranceDto);
        Task<DoctorInfoGetDto> GetDoctor(int ID);



        Task<ApiResponse> GetQuestionsByDoctorSpeciality(int docId);
        Task<ApiResponse> GetDoctorAnsweredQuestions(int docId);
        Task<ApiResponse> DoctorAnswerQuestions(AnswerDto dto);
        Task<ApiResponse> EditScheduleDoctor(int Id, DoctorEditScheduleDto doctorEditScheduleDto);
    }
}