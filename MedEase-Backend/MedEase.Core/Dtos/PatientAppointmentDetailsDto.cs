using MedEase.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class PatientAppointmentDetailsDto
    {
        public int AppointmentID { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpeciality { get; set; }
        public bool Reviewd { get; set; }
        public IEnumerable<PrescriptionDrugDto>? Prescription { get; set; }
        public string? DiagnosisDetails { get; set; }
    }
}
