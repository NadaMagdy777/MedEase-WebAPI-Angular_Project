using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Insurance
    {
        public int ID { get; set; }
        public string Company { get; set; }
        public virtual List<DoctorInsurance> Doctors { get; set; }
        public virtual List<PatientInsurance> Patients { get; set; }

        public virtual List<AppointmentInsurance> Appointments { get; set; }


    }
}
