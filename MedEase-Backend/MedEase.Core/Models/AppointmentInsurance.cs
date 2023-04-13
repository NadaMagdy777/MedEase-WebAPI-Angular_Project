using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class AppointmentInsurance
    {
        public int ID { get; set; }

        [ForeignKey("Insurance")]
        public int InsuranceID { get; set; }
        public virtual Insurance Insurance { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentID { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}
