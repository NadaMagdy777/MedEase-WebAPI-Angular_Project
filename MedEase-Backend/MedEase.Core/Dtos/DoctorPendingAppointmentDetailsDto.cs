using MedEase.Core.Consts;

namespace MedEase.Core.Dtos
{
    public class DoctorPendingAppointmentDetailsDto
    {
        public int AppointmentID { get; set; }
        public string Date { get; set; }
        public Status Status { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public DateTime PatientBirthDate { get; set; }
        public Gender PatientGender { get; set; }
        public AppointmentInvestigationDto investigation { get; set; }
        public IEnumerable<DiagnosisDto> PreviousDiagnoses { get; set; }
        public PatientMedicalHistoryDto History { get; set; }
    }
}
