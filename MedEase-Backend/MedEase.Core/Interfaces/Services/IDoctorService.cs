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
        Task<int> EditDoctor(DoctorEditDto doctorDto, int id);
        Task<int> AddDoctorSubspiciality(int DoctorID, SubspecialityDto subspeciality);
        Task<int> AddDoctorCertificate(int DoctorID, CertificateDto certificate);
        Task<int> AddDoctorInsurance(int DoctorID, int insurnceID);        
        Task<DoctorInfoGetDto> GetDoctor(int ID);
        Task<ApiResponse> GetQuestionsByDoctorSpeciality(int docId);
        Task<ApiResponse> GetDoctorAnsweredQuestions(int docId);
        Task<ApiResponse> DoctorAnswerQuestions(AnswerDto dto);
        Task<ApiResponse> EditScheduleDoctor(int Id, DoctorEditScheduleDto doctorEditScheduleDto);
        Task<ApiResponse> GetSpecialities();
    }
}