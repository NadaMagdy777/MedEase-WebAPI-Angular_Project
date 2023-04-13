using MedEase.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Appointment
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public bool PatientConfirmation { get; set; }
        public bool DoctorConfirmation { get; set; }
       
        [ForeignKey("Patient")]
        public virtual int PatientID { get; set; }
        public virtual Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        public virtual int DoctorID { get; set; }
        public virtual Doctor Doctor { get; set; }

        public AppointmentInsurance Insurance { get; set; }
    }
}
