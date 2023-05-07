using MedEase.Core;
using MedEase.Core.Dtos;

using MedEase.Core.Dtos;
using MedEase.Core.Models;

namespace MedEase.EF.Services
{
    public interface IDoctorService
    {
        Task<List<DoctorAppointmentAndPatternDto>> GetPatternAndAppointmentAsync(int Id);
        Task<DoctorSchedule> CreateScheduleAsync(DoctorScheduleDto ScheduleDto);
        Task<List<DoctorInfoGetDto>> GetTopRatedDoctors();
        Task<List<DoctorInfoGetDto>> GetAllDoctors();
        Task<IEnumerable<ReviewDto>> GetDoctorReviews(int Id);
        Task<ApiResponse> CreateReview(ReviewDto dto);
        Task<int> EditDoctor(DoctorEditDto doctorDto, int id);
        Task<int> AddDoctorSubspiciality(int DoctorID, SubspecialityDto subspeciality);
        Task<int> AddDoctorCertificate(int DoctorID, CertificateDto certificate);
        Task<int> AddDoctorInsurance(int DoctorID, int insurnceID);        
        Task<DoctorInfoGetDto> GetDoctor(int ID);
        Task<ApiResponse> EditScheduleDoctor(int Id, DoctorEditScheduleDto doctorEditScheduleDto);
        Task<ApiResponse> GetSpecialities();
        Task<PrescriptionDrug> CreatePrescriptionAsync(PrescriptionDrugDto prescriptionDto);
        Task<DiagnosisDto> CreateDiagnosisAsync(DiagnosisDto diagnosisDto);
        Task <IEnumerable<DoctorEditScheduleDto>> GetDoctorSchedules(int doctorId);
        //Task<Appointment> ReserveAppointmentAsync(ReserveAppointmentDto appointmentDto);
        //Task<ApiResponse> GetQuestionsByDoctorSpeciality(int docId);
        //Task<ApiResponse> GetDoctorAnsweredQuestions(int docId);
        //Task<ApiResponse> DoctorAnswerQuestions(AnswerDto dto);
        //Task<IEnumerable<AppointmentStatusDto>> GetPendingAppointmentsAsync(int Id);
        //Task<IEnumerable<AppointmentStatusDto>> GetConfirmedAppointmentsAsync(int Id);
        //Task<ApiResponse> GetPendingAppointmentsAsync2(int Id);
        //Task<ApiResponse> GetConfirmedAppointmentsAsync2(int docId);
        //Task<Examination> CreateExaminationAsync(ExaminationDto examinationDto);

    }
}