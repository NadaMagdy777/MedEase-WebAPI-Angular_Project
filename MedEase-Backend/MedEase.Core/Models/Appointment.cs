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
        public int id { get; set; }
        public DateTime date { get; set; }
        public Status status { get; set; }
        public bool patientConfirmation { get; set; }
        public bool doctorConfirmation { get; set; }
       
        [ForeignKey("Patient")]
        public virtual int patientID { get; set; }
        public virtual Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        public virtual int doctorID { get; set; }
        public virtual Doctor Doctor { get; set; }

        public AppointmentInsurance Insurance { get; set; }
    }
}
