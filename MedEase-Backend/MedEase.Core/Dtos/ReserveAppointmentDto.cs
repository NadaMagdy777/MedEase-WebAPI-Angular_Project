using MedEase.Core.Consts;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class ReserveAppointmentDto
    {
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public bool PatientConfirmation { get; set; }
        public bool DoctorConfirmation { get; set; }

        public int PatientID { get; set; }
        //public virtual Patient Patient { get; set; }

        public int DoctorID { get; set; }
        //public virtual Doctor Doctor { get; set; }

        //public AppointmentInsurance Insurance { get; set; }
    }
}
