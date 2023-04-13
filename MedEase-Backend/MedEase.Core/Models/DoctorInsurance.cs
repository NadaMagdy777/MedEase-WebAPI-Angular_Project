using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class DoctorInsurance
    {
        [Column(Order = 1), ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        public virtual Doctor Doctor { get; set; }

        [Column(Order = 0), ForeignKey("Insurance")]
        public int InsuranceID { get; set; }
        public virtual Insurance Insurance { get; set; }
    }
}
