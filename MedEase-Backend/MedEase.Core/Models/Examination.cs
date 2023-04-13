using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Examination
    {
        public int ID { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; }

        [ForeignKey("Review")]
        public int ReviewID { get; set; }
        public Review Review { get; set; }


        [ForeignKey("Diagnosis")]
        public int DiagnosisID { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public List<PrescriptionDrug> PrescribedDrugs { get; set; }
    }
}
