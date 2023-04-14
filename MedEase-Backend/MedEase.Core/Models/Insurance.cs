using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Insurance
    {
        public int ID { get; set; }
        public string Company { get; set; }

        [JsonIgnore]
        public virtual List<DoctorInsurance> Doctors { get; set; }

        [JsonIgnore]

        public virtual List<PatientInsurance> Patients { get; set; }
        [JsonIgnore]

        public virtual List<AppointmentInsurance> Appointments { get; set; }


    }
}
