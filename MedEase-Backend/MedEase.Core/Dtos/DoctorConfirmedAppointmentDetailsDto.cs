using MedEase.Core.Consts;

namespace MedEase.Core.Dtos
{
    public class DoctorConfirmedAppointmentDetailsDto
    {
        public int AppointmentID { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public DateTime PatientBirthDate { get; set; }
        public Gender PatientGender { get; set; }
        public DiagnosisDto Diagnosis { get; set; }
        public IEnumerable<PrescriptionDrugDto> Prescription { get; set; }
    }
}
