using MedEase.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Interfaces.Services
{
    public interface IAppointmentService
    {
        Task<ApiResponse> GetPatientPendingAppointmentsAsync(int patientID);
        Task<ApiResponse> GetPatientConfirmedAppointmentsAsync(int patientID);
        Task<ApiResponse> GetDoctorPendingAppointmentsAsync(int Id);
        Task<ApiResponse> GetDoctorConfirmedAppointmentsAsync(int docId);
        Task<ApiResponse> ReserveAppointment(AppointmentReservationDto dto);
        Task<ApiResponse> DoctorAppointmentAction(AppointmentActionDto dto);
        Task<ApiResponse> PatientAppointmentAction(AppointmentActionDto dto);
    }
}
