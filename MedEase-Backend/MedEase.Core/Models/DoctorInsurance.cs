using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class DoctorInsurance
    {
        public int ID { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("Insurance")]
        public int InsuranceID { get; set; }
        public virtual Insurance Insurance { get; set; }
    }
}
